using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Guides.Backend.Domain;
using Guides.Backend.Exceptions.Auth;
using Guides.Backend.Repositories.Auth;
using Guides.Backend.StaticProviders;
using Guides.Backend.ViewModels.Auth;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Guides.Backend.Services.Auth
{
    public abstract class AuthServiceBase
    {
        private readonly IAuthRepository _repository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger _logger;

        protected AuthServiceBase(
            IAuthRepository repository, 
            ILoggerFactory loggerFactory, 
            IMapper mapper, 
            IWebHostEnvironment environment)
        {
            _repository = repository;
            _mapper = mapper;
            _environment = environment;
            _logger = loggerFactory.CreateLogger(GeneralStaticDataProvider.AuthLogCategory);
        }

        protected async Task<AuthTokenViewModel> Login(AuthLoginViewModel viewModel, AuthRegionViewModel region)
        {
            var userDb = await _repository.GetUserByEmail(viewModel.Username);

            if (userDb == null)
            {
                _logger.LogInformation($"Login tried with non-existing username: ${viewModel.Username}");
                throw new LoginFailedException();
            }

            if (!IsRegionAllowed(userDb.Country, region))
            {
                _logger.LogInformation($"Cross region login detected for username: ${viewModel.Username} at region: {region?.Country}");
                throw new LoginFailedException();
            }

            if (userDb.IsAdminLocked || userDb.IsLoginLocked)
            {
                _logger.LogInformation($"Login tried for blocked username: ${viewModel.Username}");
                throw new LoginFailedException();
            }


            if (VerifyPasswordHash(viewModel.Password, userDb.PasswordHash, userDb.PasswordSalt))
            {

                await RecordCleanLoginAttempt(userDb);
                _logger.LogInformation($"Login processed for user {viewModel.Username}");
                return new AuthTokenViewModel
                {
                    Jwt = GetToken(new AuthUserClaimsViewModel
                    {
                        Email = viewModel.Username,
                        DisplayName = userDb.DisplayName,
                        ProfilePhotographUrl = userDb.ImageUrl,
                        Country = userDb.Country.ToString(),
                        Roles = _repository.GetRolesForUser(userDb)
                    })
                };
            }

            await RecordFailedLoginAttempt(userDb);
            _logger.LogInformation($"Login tried with wrong password for username: ${viewModel.Username}");
            throw new LoginFailedException();
        }

        protected async Task<AuthResetKeyViewModel> Register(AuthRegisterViewModel viewModel, AuthRegionViewModel region)
        {
            if (!IsRegionAllowed(viewModel.Country, region))
            {
                _logger.LogInformation($"Cross region registration detected for username: ${viewModel.Email} at region: {region?.Country}");
                throw new LoginFailedException();
            }
            
            if (await _repository.UserExists(viewModel.MobileNumber))
            {
                _logger.LogInformation($"Duplicate registration for mobile number {viewModel.MobileNumber} prevented [{viewModel.Email}]");
                throw new RegistrationFailedException();

            }

            if (await _repository.UserExists(viewModel.Email))
            {
                _logger.LogInformation($"Duplicate registration for email {viewModel.Email} prevented");
                throw new RegistrationFailedException();

            }

            var user = _mapper.Map<AuthRegisterViewModel, User>(viewModel);
            
            if (!string.IsNullOrWhiteSpace(viewModel.ImageBase64))
            {
                //  profile photograph and thumbnail
                user.ImageUrl = Guid.NewGuid().ToString();
                var fileName = Path.Combine(_environment.WebRootPath, $"{user.ImageUrl}.jpg");
                var fileNameTN = Path.Combine(_environment.WebRootPath, $"{user.ImageUrl}-thumbnail.jpg");

                await StoreImage(viewModel.ImageBase64, fileName);
                SaveThumbnail(fileName, fileNameTN);
            }

            _logger.LogInformation($"Preparing for registration of {viewModel.Email}");
            user.CreatedOn = DateTime.UtcNow;
            user.IsLoginLocked = true;
            user.ResetKey = GeneralStaticDataProvider.GetNewResetKey();
            user.ResetKeyExpiresOn = DateTime.UtcNow.AddHours(GeneralStaticDataProvider.ResetKeyExpiresInHours);
            user.LockedOn = DateTime.UtcNow;
            user.LastUpdateOn = DateTime.UtcNow;

            _logger.LogInformation($"Preparing role enrollment for registration of {viewModel.Email}");

            foreach (var role in viewModel.Roles)
            {
                var roleDb = await _repository.GetRoleByName(role);

                if (roleDb == null)
                {
                    _logger.LogInformation($"Non-existent role {role} sought for registration of {viewModel.Email}");
                    throw new RegistrationFailedException();
                }

                if (!CanRegisterForRole(role, region))
                {
                    _logger.LogInformation($"Prevented creation of prohibited role {role} for {viewModel.Email}");
                    throw new RegistrationFailedException();
                }
                
                user.UserRoles.Add(new UserRole
                {
                    User = user,
                    Role = roleDb
                });
            }

            _logger.LogInformation($"Initiating persistence for registration of {viewModel.Email}");

            await _repository.AddUser(user);
            _logger.LogInformation($"User registration succeeded for {viewModel.Email}");
            return new AuthResetKeyViewModel { ResetKey = user.ResetKey };
        }

        protected async Task ChangePassword(AuthChangePasswordViewModel viewModel, AuthRegionViewModel region)
        {
            if (await IsLoginBlocked(viewModel.Username))
            {
                _logger.LogInformation($"Change password prevented for locked/non-existent user: {viewModel.Username}");
                throw new LoginFailedException();
            }

            var userDb = await _repository.GetUserByEmail(viewModel.Username);
            
            if (!IsRegionAllowed(userDb.Country, region))
            {
                _logger.LogInformation($"Cross region change password detected for username: ${viewModel.Username} at region: {region?.Country}");
                throw new LoginFailedException();
            }

            if (viewModel.NewPassword != viewModel.NewPasswordRepeat)
            {
                _logger.LogInformation($"Change password prevented as new passwords do not match for user: {viewModel.Username}");
                throw new DomainValidationException();
            }

            if (VerifyPasswordHash(viewModel.CurrentPassword, userDb.PasswordHash, userDb.PasswordSalt))
            {
                _logger.LogInformation($"Change password final steps initiated for user {viewModel.Username}");
                CreatePasswordHash(viewModel.NewPassword, out var newPasswordHash, out var newPasswordSalt);

                userDb.PasswordHash = newPasswordHash;
                userDb.PasswordSalt = newPasswordSalt;
                userDb.PasswordResetOn = DateTime.UtcNow;

                await _repository.UpdateUser(userDb);
                _logger.LogInformation($"Change password completed for user {viewModel.Username}");

                return; // else, it will fall through!
            }

            _logger.LogInformation($"Change password prevented as the current password is wrong for user: {viewModel.Username}");
            await RecordFailedLoginAttempt(userDb);
            throw new LoginFailedException();
        }

        protected async Task Update(AuthUpdateViewModel viewModel, AuthRegionViewModel region)
        {
            var userDb = await this._repository.GetUserByEmail(viewModel.Username);

            if (userDb == null)
            {
                this._logger.LogInformation($"Update prevented for non-existent user {viewModel.Username}");
                throw new AdminActionNotSupportedException();
            }
            
            if (!IsRegionAllowed(userDb.Country, region))
            {
                _logger.LogInformation($"Cross region update detected for username: ${viewModel.Username} at region: {region?.Country}");
                throw new AdminActionNotSupportedException();
            }
            
            if (userDb.IsAdminLocked)
            {
                _logger.LogInformation($"Update prevented for admin blocked user: {viewModel.Username}");
                throw new AdminActionNotSupportedException();
            }
            
            this._logger.LogInformation($"Update process start for username: {viewModel.Username}");
            userDb.FullName = viewModel.FullName;
            userDb.MobileNumber = viewModel.MobileNumber;
            userDb.IdentityInformation = viewModel.IdentityInformation;
            userDb.OfficialPosition = viewModel.OfficialPosition;
            userDb.LastUpdateOn = DateTime.UtcNow;
            
            //  Image
            if (!string.IsNullOrWhiteSpace(viewModel.ImageBase64))
            {
                try
                {
                    //  delete old profile photo files
                    if (!string.IsNullOrWhiteSpace(userDb.ImageUrl))
                    {
                        var fileNameOriginal = Path.Combine(_environment.WebRootPath, $"{userDb.ImageUrl}.jpg");
                        var fileNameTNOriginal = Path.Combine(_environment.WebRootPath, $"{userDb.ImageUrl}-thumbnail.jpg");

                        var imageExists = System.IO.File.Exists(fileNameOriginal);
                        var tnExists = System.IO.File.Exists(fileNameTNOriginal);

                        if (imageExists)
                        {
                            System.IO.File.Delete(fileNameOriginal);
                        }

                        if (tnExists)
                        {
                            System.IO.File.Delete(fileNameTNOriginal);
                        }
                    }


                    //  set profile photograph and thumbnail afresh
                    userDb.ImageUrl = Guid.NewGuid().ToString();
                    var fileName = Path.Combine(_environment.WebRootPath, $"{userDb.ImageUrl}.jpg");
                    var fileNameTN = Path.Combine(_environment.WebRootPath, $"{userDb.ImageUrl}-thumbnail.jpg");

                    await StoreImage(viewModel.ImageBase64, fileName);

                    SaveThumbnail(fileName, fileNameTN);
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message, e);
                }
            }

            await this._repository.UpdateUser(userDb);

            _logger.LogInformation($"User update request completed for username: {viewModel.Username}");
        }
        protected async Task<AuthResetKeyViewModel> AdminReset(AuthAdminActionViewModel viewModel, AuthRegionViewModel region)
        {
            var userDb = await _repository.GetUserByEmail(viewModel.Username);

            if (userDb == null)
            {
                _logger.LogInformation($"Admin reset prevented for non-existent user: {viewModel.Username}");
                throw new AdminActionNotSupportedException();
            }
            
            if (!IsRegionAllowed(userDb.Country, region))
            {
                _logger.LogInformation($"Cross region admin reset detected for username: ${viewModel.Username} at region: {region?.Country}");
                throw new AdminActionNotSupportedException();
            }

            if (userDb.IsAdminLocked)
            {
                _logger.LogInformation($"Admin reset initiated for user {viewModel.Username}");

                userDb.PasswordHash = null;
                userDb.PasswordSalt = null;
                userDb.AdminResetOn = DateTime.UtcNow;
                userDb.LoginResetOn = DateTime.UtcNow;
                userDb.ResetKey = GeneralStaticDataProvider.GetNewResetKey();
                userDb.ResetKeyExpiresOn = DateTime.UtcNow.AddHours(GeneralStaticDataProvider.ResetKeyExpiresInHours);
                userDb.IsLoginLocked = true;
                userDb.IsAdminLocked = false;   //  release
                userDb.FailedAttempts = 0;

                await _repository.UpdateUser(userDb);
                _logger.LogInformation($"Admin reset completed for user {viewModel.Username}");

                return new AuthResetKeyViewModel
                {
                    ResetKey = userDb.ResetKey
                };
            }

            _logger.LogInformation($"No admin block to reset for user: {viewModel.Username}");
            throw new AdminActionNotSupportedException();
        }

        protected async Task<AuthResetKeyViewModel> LoginReset(AuthAdminActionViewModel viewModel, AuthRegionViewModel region)
        {
            var userDb = await _repository.GetUserByEmail(viewModel.Username);

            if (userDb == null)
            {
                _logger.LogInformation($"Login reset prevented for non-existent user: {viewModel.Username}");
                throw new AdminActionNotSupportedException();
            }
            
            if (!IsRegionAllowed(userDb.Country, region))
            {
                _logger.LogInformation($"Cross region login reset detected for username: ${viewModel.Username} at region: {region?.Country}");
                throw new AdminActionNotSupportedException();
            }

            if (userDb.IsAdminLocked)
            {
                _logger.LogInformation($"Login reset prevented for admin blocked user: {viewModel.Username}");
                throw new AdminActionNotSupportedException();
            }

            if (userDb.IsLoginLocked)
            {
                _logger.LogInformation($"Login reset initiated for user {viewModel.Username}");

                userDb.PasswordHash = null;
                userDb.PasswordSalt = null;
                userDb.LoginResetOn = DateTime.UtcNow;
                userDb.ResetKey = GeneralStaticDataProvider.GetNewResetKey();
                userDb.ResetKeyExpiresOn = DateTime.UtcNow.AddHours(GeneralStaticDataProvider.ResetKeyExpiresInHours);
                userDb.FailedAttempts = 0;

                await _repository.UpdateUser(userDb);
                _logger.LogInformation($"Login reset completed for user {viewModel.Username}");

                return new AuthResetKeyViewModel
                {
                    ResetKey = userDb.ResetKey
                };
            }

            _logger.LogInformation($"No login block to reset for user: {viewModel.Username}");
            throw new AdminActionNotSupportedException();
        }

        protected async Task AdminBlock(AuthAdminActionViewModel viewModel, AuthRegionViewModel region)
        {
            var userDb = await _repository.GetUserByEmail(viewModel.Username);

            if (userDb == null)
            {
                _logger.LogInformation($"Admin block prevented for non-existent user: {viewModel.Username}");
                throw new AdminActionNotSupportedException();
            }
            
            if (!IsRegionAllowed(userDb.Country, region))
            {
                _logger.LogInformation($"Cross region admin block detected for username: ${viewModel.Username} at region: {region?.Country}");
                throw new AdminActionNotSupportedException();
            }

            if (userDb.IsAdminLocked)
            {
                _logger.LogInformation($"Admin block prevented for already blocked user: {viewModel.Username}");
                return; //  return silently
            }

            _logger.LogInformation($"Admin block initiated for user {viewModel.Username}");

            userDb.AdminBlockOn = DateTime.UtcNow;
            userDb.LoginBlockOn = DateTime.UtcNow;
            userDb.IsLoginLocked = true;
            userDb.IsAdminLocked = true;

            await _repository.UpdateUser(userDb);
            _logger.LogInformation($"Admin block completed for user {viewModel.Username}");
        }

        protected async Task ResetPassword(AuthResetPasswordViewModel viewModel, AuthRegionViewModel region)
        {
            var userDb = await _repository.GetUserByEmail(viewModel.Username);

            if (userDb == null)
            {
                _logger.LogInformation($"Password reset prevented for non-existent user: {viewModel.Username}");
                throw new UserActionNotSupportedException();
            }
            
            if (!IsRegionAllowed(userDb.Country, region))
            {
                _logger.LogInformation($"Cross region password reset detected for username: ${viewModel.Username} at region: {region?.Country}");
                throw new UserActionNotSupportedException();
            }

            if (userDb.IsAdminLocked)
            {
                _logger.LogInformation($"Password reset prevented for admin blocked user: {viewModel.Username}");
                throw new UserActionNotSupportedException();
            }

            if (userDb.IsLoginLocked)
            {
                if (MatchResetKey(userDb, viewModel.ResetKey))
                {
                    await ResetPasswordDirect(viewModel);
                    return; //  prevent fallthrough
                }

                _logger.LogInformation($"Reset key discrepancy detected and attempt prevented for : {viewModel.Username}");
                throw new UserActionPreventedException();
            }

            _logger.LogInformation($"No action taken for password reset for non-blocked user: {viewModel.Username}");
            throw new UserActionNotSupportedException();
        }

        private async Task<bool> IsLoginBlocked(string email)
        {
            var userDb = await _repository.GetUserByEmail(email);
            return userDb != null && (userDb.IsLoginLocked || userDb.IsAdminLocked);
        }

        private static string GetToken(AuthUserClaimsViewModel viewModel)
        {
            var claims = new List<Claim>
            {
                new Claim("email", viewModel.Email),
                new Claim("displayName", viewModel.DisplayName),
                new Claim("country", viewModel.Country),
            };
            
            if (!string.IsNullOrWhiteSpace(viewModel.ProfilePhotographUrl))
            {
                claims.Add(new Claim("imageUrl", viewModel.ProfilePhotographUrl));
            }

            foreach (var role in viewModel.Roles)
            {
                claims.Add(new Claim("roles", role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GeneralStaticDataProvider.GuidesEncryptionKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private static void CreatePasswordHash(string rawPassword, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(rawPassword));
        }

        private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != passwordHash[i])
                {
                    return false;
                }
            }

            return true;
        }

        private async Task RecordFailedLoginAttempt(User user)
        {
            ++user.FailedAttempts;

            if (user.FailedAttempts == GeneralStaticDataProvider.MaxFailedAttempts)
            {
                _logger.LogInformation($"Login blocked as max attempts are exhausted for username: ${user.Email}");
                user.IsLoginLocked = true;
            }

            await _repository.UpdateUser(user);
        }

        private async Task RecordCleanLoginAttempt(User user)
        {
            if (user.FailedAttempts > 0)
            {
                _logger.LogInformation($"Clean login recorded after {user.FailedAttempts} permissible failed login attempts for username: ${user.Email}");
                user.FailedAttempts = 0;
                await _repository.UpdateUser(user);
            }
        }

        private bool MatchResetKey(User user, string resetKey)
        {
            if (string.IsNullOrEmpty(user.ResetKey))
            {
                _logger.LogInformation($"Reset key does not exist for user {user.Email}");
                return false;
            }

            if (string.IsNullOrEmpty(resetKey))
            {
                _logger.LogInformation($"Reset key not provided by the user {user.Email}");
                return false;
            }

            if (user.ResetKey == resetKey)
            {
                if (user.ResetKeyExpiresOn >= DateTime.UtcNow)
                {
                    return true;
                }

                _logger.LogInformation($"Reset key expired for the user {user.Email}");
                return false;
            }

            _logger.LogInformation($"Reset key does not match for the user {user.Email}");
            return false;
        }

        private async Task ResetPasswordDirect(AuthResetPasswordViewModel viewModel)
        {
            if (viewModel.NewPassword != viewModel.NewPasswordRepeat)
            {
                _logger.LogInformation($"Reset password prevented as new passwords do not match for user: {viewModel.Username}");
                throw new DomainValidationException();
            }

            _logger.LogInformation($"Reset password final steps initiated for user: {viewModel.Username}");
            var userDb = await _repository.GetUserByEmail(viewModel.Username);

            CreatePasswordHash(viewModel.NewPassword, out var passwordHash, out var passwordSalt);

            userDb.IsLoginLocked = false;
            userDb.ResetKey = null;
            userDb.LockedOn = null;
            userDb.PasswordResetOn = DateTime.UtcNow;
            userDb.ResetKeyExpiresOn = null;
            userDb.PasswordHash = passwordHash;
            userDb.PasswordSalt = passwordSalt;

            _logger.LogInformation($"Reset password completed for user: {viewModel.Username}");
            await _repository.UpdateUser(userDb);
        }

        private bool IsRegionAllowed(Country sourceCountry, AuthRegionViewModel region)
        {
            if (region.IsMaster)
            {
                return true;
            }

            if (region.Country == null)
            {
                return false;
            }

            return region.Country == sourceCountry;
        }
        
        private bool IsRegionAllowed(string sourceCountry, AuthRegionViewModel region)
        {
            if (region.IsMaster)
            {
                return true;
            }

            if (region.Country == null)
            {
                return false;
            }

            var conversionResult = Enum.TryParse<Country>(sourceCountry, out var country);

            return conversionResult && region.Country == country;
        }

        private bool CanRegisterForRole(string role, AuthRegionViewModel viewModel)
        {
            if (viewModel.IsMaster)
            {
                return true;
            }

            return !IsRoleAnAdministrator(role);
        }

        private bool IsRoleAnAdministrator(string role)
        {
            return GeneralStaticDataProvider.GeneralAdministratorRoles.Split(',').Any(r => r == role)
                   || GeneralStaticDataProvider.IndiaAdministratorRoles.Split(',').Any(r => r == role)
                   || GeneralStaticDataProvider.UgandaAdministratorRoles.Split(',').Any(r => r == role);
        }
        
        private static async Task StoreImage(string base64String, string path)
        {
            var fileBytes = Convert.FromBase64String(base64String);
            await System.IO.File.WriteAllBytesAsync(path, fileBytes);
        }
        
        private void SaveThumbnail(string originalFileName, string thumbnailFileName)
        {
            using (var image = Image.Load(originalFileName))
            {
                var originalHeight = image.Height;
                var origImageWidth = image.Width;

                int? newHeight = null;
                int? newWidth = null;



                if (originalHeight >= origImageWidth)
                {
                    var ratio = Convert.ToDouble(origImageWidth) / originalHeight;
                    newHeight = GeneralStaticDataProvider.THUMBNAIL_SIZE;
                    newWidth = Convert.ToInt32(GeneralStaticDataProvider.THUMBNAIL_SIZE * ratio);
                }
                else
                {
                    var ratio = Convert.ToDouble(originalHeight) / origImageWidth;
                    newWidth = GeneralStaticDataProvider.THUMBNAIL_SIZE;
                    newHeight = Convert.ToInt32(GeneralStaticDataProvider.THUMBNAIL_SIZE * ratio);
                }

                image.Mutate(x => x
                    .Resize(newWidth.Value, newHeight.Value)
                );
                image.Save(thumbnailFileName); // Automatic encoder selected based on extension.
            }
        }
    }
}
