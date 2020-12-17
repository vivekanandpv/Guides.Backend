using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Guides.Backend.Domain;
using Guides.Backend.Exceptions.Auth;
using Guides.Backend.Repositories.Auth;
using Guides.Backend.StaticProviders;
using Guides.Backend.ViewModels.Auth;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Guides.Backend.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public AuthService(IAuthRepository repository, ILoggerFactory loggerFactory, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = loggerFactory.CreateLogger(GeneralStaticDataProvider.AuthLogCategory);
        }

        public async Task<AuthTokenViewModel> Login(AuthLoginViewModel viewModel)
        {
            var userDb = await this._repository.GetUserByEmail(viewModel.Username);

            if (userDb == null)
            {
                this._logger.LogInformation($"Login tried with non-existing username: ${viewModel.Username}");
                throw new LoginFailedException();
            }

            if (userDb.IsAdminLocked || userDb.IsLoginLocked)
            {
                this._logger.LogInformation($"Login tried for blocked username: ${viewModel.Username}");
                throw new LoginFailedException();
            }


            if (VerifyPasswordHash(viewModel.Password, userDb.PasswordHash, userDb.PasswordSalt))
            {
                await RecordCleanLoginAttempt(userDb);

                return new AuthTokenViewModel
                {
                    Jwt = GetToken(new AuthUserClaimsViewModel
                    {
                        Email = viewModel.Username,
                        DisplayName = userDb.DisplayName,
                        Roles = _repository.GetRolesForUser(userDb)
                    })
                };
            }

            await RecordFailedLoginAttempt(userDb);
            this._logger.LogInformation($"Login tried with wrong password for username: ${viewModel.Username}");
            throw new LoginFailedException();
        }

        public async Task<AuthResetKeyViewModel> Register(AuthRegisterViewModel viewModel)
        {
            if (await this._repository.UserExists(viewModel.MobileNumber))
            {
                this._logger.LogInformation($"Duplicate registration for mobile number {viewModel.MobileNumber} prevented [{viewModel.Email}]");
                throw new RegistrationFailedException();

            }

            if (await this._repository.UserExists(viewModel.Email))
            {
                this._logger.LogInformation($"Duplicate registration for email {viewModel.Email} prevented");
                throw new RegistrationFailedException();

            }

            CreatePasswordHash(viewModel.Password, out var passwordHash, out var passwordSalt);

            var user = this._mapper.Map<AuthRegisterViewModel, User>(viewModel);

            this._logger.LogInformation($"Preparing for registration of {viewModel.Email}");
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.CreatedOn = DateTime.UtcNow;
            user.IsLoginLocked = true;
            user.ResetKey = GeneralStaticDataProvider.GetNewResetKey();
            user.ResetKeyExpiresOn = DateTime.UtcNow.AddHours(GeneralStaticDataProvider.ResetKeyExpiresInHours);
            user.LockedOn = DateTime.UtcNow;

            this._logger.LogInformation($"Preparing role enrollment for registration of {viewModel.Email}");

            foreach (var role in viewModel.Roles)
            {
                var roleDb = await this._repository.GetRoleByName(role);

                if (roleDb == null)
                {
                    this._logger.LogInformation($"Non-existent role {role} sought for registration of {viewModel.Email}");
                    throw new RegistrationFailedException();
                }

                user.UserRoles.Add(new UserRole
                {
                    User = user,
                    Role = roleDb
                });
            }

            this._logger.LogInformation($"Initiating persistence for registration of {viewModel.Email}");

            await this._repository.AddUser(user);
            this._logger.LogInformation($"User registration succeeded for {viewModel.Email}");
            return new AuthResetKeyViewModel { ResetKey = user.ResetKey };
        }

        public async Task ChangePassword(AuthChangePasswordViewModel viewModel)
        {
            if (await this.IsLoginBlocked(viewModel.Username))
            {
                this._logger.LogInformation($"Change password prevented for locked/non-existent user: {viewModel.Username}");
                throw new LoginFailedException();
            }

            var userDb = await this._repository.GetUserByEmail(viewModel.Username);

            if (viewModel.NewPassword != viewModel.NewPasswordRepeat)
            {
                this._logger.LogInformation($"Change password prevented as new passwords do not match for user: {viewModel.Username}");
                throw new DomainValidationException();
            }

            if (VerifyPasswordHash(viewModel.CurrentPassword, userDb.PasswordHash, userDb.PasswordSalt))
            {
                this._logger.LogInformation($"Change password final steps initiated for user {viewModel.Username}");
                CreatePasswordHash(viewModel.NewPassword, out var newPasswordHash, out var newPasswordSalt);

                userDb.PasswordHash = newPasswordHash;
                userDb.PasswordSalt = newPasswordSalt;
                userDb.PasswordResetOn = DateTime.UtcNow;

                await this._repository.UpdateUser(userDb);
                this._logger.LogInformation($"Change password completed for user {viewModel.Username}");
            }

            this._logger.LogInformation($"Change password prevented as the current password is wrong for user: {viewModel.Username}");
            await this.RecordFailedLoginAttempt(userDb);
            throw new LoginFailedException();
        }

        public async Task<AuthResetKeyViewModel> AdminReset(string email)
        {
            var userDb = await this._repository.GetUserByEmail(email);

            if (userDb == null)
            {
                this._logger.LogInformation($"Admin reset prevented for non-existent user: {email}");
                throw new AdminActionNotSupportedException();
            }

            if (userDb.IsAdminLocked)
            {
                this._logger.LogInformation($"Admin reset initiated for user {email}");

                userDb.PasswordHash = null;
                userDb.PasswordSalt = null;
                userDb.AdminResetOn = DateTime.UtcNow;
                userDb.LoginResetOn = DateTime.UtcNow;
                userDb.ResetKey = GeneralStaticDataProvider.GetNewResetKey();
                userDb.ResetKeyExpiresOn = DateTime.UtcNow.AddHours(GeneralStaticDataProvider.ResetKeyExpiresInHours);
                userDb.IsLoginLocked = true;
                userDb.IsAdminLocked = false;   //  release
                userDb.FailedAttempts = 0;

                await this._repository.UpdateUser(userDb);
                this._logger.LogInformation($"Admin reset completed for user {email}");

                return new AuthResetKeyViewModel
                {
                    ResetKey = userDb.ResetKey
                };
            }

            this._logger.LogInformation($"No admin block to reset for user: {email}");
            throw new AdminActionNotSupportedException();
        }

        public async Task<AuthResetKeyViewModel> LoginReset(string email)
        {
            var userDb = await this._repository.GetUserByEmail(email);

            if (userDb == null)
            {
                this._logger.LogInformation($"Login reset prevented for non-existent user: {email}");
                throw new AdminActionNotSupportedException();
            }

            if (userDb.IsAdminLocked)
            {
                this._logger.LogInformation($"Login reset prevented for admin blocked user: {email}");
                throw new AdminActionNotSupportedException();
            }

            if (userDb.IsLoginLocked)
            {
                this._logger.LogInformation($"Login reset initiated for user {email}");

                userDb.PasswordHash = null;
                userDb.PasswordSalt = null;
                userDb.LoginResetOn = DateTime.UtcNow;
                userDb.ResetKey = GeneralStaticDataProvider.GetNewResetKey();
                userDb.ResetKeyExpiresOn = DateTime.UtcNow.AddHours(GeneralStaticDataProvider.ResetKeyExpiresInHours);
                userDb.FailedAttempts = 0;

                await this._repository.UpdateUser(userDb);
                this._logger.LogInformation($"Login reset completed for user {email}");

                return new AuthResetKeyViewModel
                {
                    ResetKey = userDb.ResetKey
                };
            }

            this._logger.LogInformation($"No login block to reset for user: {email}");
            throw new AdminActionNotSupportedException();
        }

        public async Task AdminBlock(string email)
        {
            var userDb = await this._repository.GetUserByEmail(email);

            if (userDb == null)
            {
                this._logger.LogInformation($"Admin block prevented for non-existent user: {email}");
                throw new AdminActionNotSupportedException();
            }

            if (userDb.IsAdminLocked)
            {
                this._logger.LogInformation($"Admin block prevented for already blocked user: {email}");
                return; //  return silently
            }

            this._logger.LogInformation($"Admin block initiated for user {email}");

            userDb.AdminBlockOn = DateTime.UtcNow;
            userDb.LoginBlockOn = DateTime.UtcNow;
            userDb.IsLoginLocked = true;
            userDb.IsAdminLocked = true;

            await this._repository.UpdateUser(userDb);
            this._logger.LogInformation($"Admin block completed for user {email}");
        }

        public async Task ResetPassword(AuthResetPasswordViewModel viewModel)
        {
            var userDb = await this._repository.GetUserByEmail(viewModel.Username);

            if (userDb == null)
            {
                this._logger.LogInformation($"Password reset prevented for non-existent user: {viewModel.Username}");
                throw new UserActionNotSupportedException();
            }

            if (userDb.IsAdminLocked)
            {
                this._logger.LogInformation($"Password reset prevented for admin blocked user: {viewModel.Username}");
                throw new UserActionNotSupportedException();
            }

            if (userDb.IsLoginLocked)
            {
                if (MatchResetKey(userDb, viewModel.ResetKey))
                {
                    await ResetPasswordDirect(viewModel);
                    return; //  prevent fallthrough
                }
                else
                {
                    this._logger.LogInformation($"Reset key discrepancy detected and attempt prevented for : {viewModel.Username}");
                    throw new UserActionPreventedException();
                }
            }
            
            this._logger.LogInformation($"No action taken for password reset for non-blocked user: {viewModel.Username}");
            throw new UserActionNotSupportedException();
        }

        public async Task<bool> IsLoginBlocked(string email)
        {
            var userDb = await this._repository.GetUserByEmail(email);
            return userDb != null && (userDb.IsLoginLocked || userDb.IsAdminLocked);
        }

        private static string GetToken(AuthUserClaimsViewModel viewModel)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, viewModel.Email),
                new Claim(ClaimTypes.Name, viewModel.DisplayName),

            };

            foreach (var role in viewModel.Roles)
            {
                claims.Add(new Claim("Roles", role));
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
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(rawPassword));
        }

        private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

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
                this._logger.LogInformation($"Login blocked as max attempts are exhausted for username: ${user.Email}");
                user.IsLoginLocked = true;
            }

            await this._repository.UpdateUser(user);
        }

        private async Task RecordCleanLoginAttempt(User user)
        {
            if (user.FailedAttempts > 0)
            {
                this._logger.LogInformation($"Clean login recorded after {user.FailedAttempts} permissible failed login attempts for username: ${user.Email}");
                user.FailedAttempts = 0;
                await this._repository.UpdateUser(user);
            }
        }

        private bool MatchResetKey(User user, string resetKey)
        {
            if (string.IsNullOrEmpty(user.ResetKey))
            {
                this._logger.LogInformation($"Reset key does not exist for user {user.Email}");
                return false;
            }

            if (string.IsNullOrEmpty(resetKey))
            {
                this._logger.LogInformation($"Reset key not provided by the user {user.Email}");
                return false;
            }

            if (user.ResetKey == resetKey)
            {
                if (user.ResetKeyExpiresOn < DateTime.UtcNow)
                {
                    return true;
                }

                this._logger.LogInformation($"Reset key expired for the user {user.Email}");
                return false;
            }
            
            this._logger.LogInformation($"Reset key does not match for the user {user.Email}");
            return false;
        }

        private async Task ResetPasswordDirect(AuthResetPasswordViewModel viewModel)
        {
            if (viewModel.NewPassword != viewModel.NewPasswordRepeat)
            {
                this._logger.LogInformation($"Reset password prevented as new passwords do not match for user: {viewModel.Username}");
                throw new DomainValidationException();
            }

            this._logger.LogInformation($"Reset password final steps initiated for user: {viewModel.Username}");
            var userDb = await this._repository.GetUserByEmail(viewModel.Username);

            CreatePasswordHash(viewModel.NewPassword, out var passwordHash, out var passwordSalt);

            userDb.IsLoginLocked = false;
            userDb.LoginResetOn = DateTime.UtcNow;
            userDb.ResetKey = null;
            userDb.PasswordHash = passwordHash;
            userDb.PasswordSalt = passwordSalt;

            this._logger.LogInformation($"Reset password completed for user: {viewModel.Username}");
            await this._repository.UpdateUser(userDb);
        }
    }

    
}
