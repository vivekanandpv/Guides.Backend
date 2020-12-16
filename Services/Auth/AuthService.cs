using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Guides.Backend.Domain;
using Guides.Backend.Exceptions;
using Guides.Backend.Repositories.Auth;
using Guides.Backend.StaticProviders;
using Guides.Backend.ViewModels.Auth;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Guides.Backend.Services.Auth
{
    public class AuthService:IAuthService
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
            user.ResetKeyExpiresOn = DateTime.UtcNow.AddHours(1);
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
            return new AuthResetKeyViewModel {ResetKey = user.ResetKey};
        }

        public async Task ChangePassword(AuthChangePasswordViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthResetKeyViewModel> AdminReset(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthResetKeyViewModel> LoginReset(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task AdminBlock(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task ResetPassword(AuthResetPasswordViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsLoginBlocked(string email)
        {
            var userDb = await this._repository.GetUserByEmail(email);
            return userDb.IsLoginLocked || userDb.IsAdminLocked;
        }

        private string GetToken(AuthUserClaimsViewModel viewModel)
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
        
        private void CreatePasswordHash(string rawPassword, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(rawPassword));
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
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

        public async Task RecordFailedLoginAttempt(User user)
        {
            ++user.FailedAttempts;

            if (user.FailedAttempts == GeneralStaticDataProvider.MaxFailedAttempts)
            {
                this._logger.LogInformation($"Login blocked as max attempts are exhausted for username: ${user.Email}");
                user.IsLoginLocked = true;
            }
            
            await this._repository.UpdateUser(user);
        }
        
        public async Task RecordCleanLoginAttempt(User user)
        {
            if (user.FailedAttempts > 0)
            {
                this._logger.LogInformation($"Clean login recorded after {user.FailedAttempts} permissible failed login attempts for username: ${user.Email}");
                user.FailedAttempts = 0;
                await this._repository.UpdateUser(user);
            }
        }
    }

    
}
