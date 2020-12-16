using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Guides.Backend.Domain;
using Guides.Backend.StaticProviders;

namespace Guides.Backend.ViewModels.Auth
{
    public class AuthResetPasswordViewModel
    {
        [Required, MaxLength(10)]
        public string ResetKey { get; set; }
        [Required, MaxLength(50), RegularExpression(pattern:GeneralStaticDataProvider.PasswordPolicyRegEx, ErrorMessage = GeneralStaticDataProvider.PasswordPolicyErrorMessage)]
        public string NewPassword { get; set; }
        [Required, MaxLength(50), RegularExpression(pattern:GeneralStaticDataProvider.PasswordPolicyRegEx, ErrorMessage = GeneralStaticDataProvider.PasswordPolicyErrorMessage)]
        public string NewPasswordRepeat { get; set; }
    }

    public class AuthResetKeyViewModel
    {
        public string ResetKey { get; set; }
    }

    public class AuthChangePasswordViewModel
    {
        [Required, MaxLength(50), RegularExpression(pattern:GeneralStaticDataProvider.PasswordPolicyRegEx, ErrorMessage = GeneralStaticDataProvider.PasswordPolicyErrorMessage)]
        public string CurrentPassword { get; set; }
        [Required, MaxLength(50), RegularExpression(pattern:GeneralStaticDataProvider.PasswordPolicyRegEx, ErrorMessage = GeneralStaticDataProvider.PasswordPolicyErrorMessage)]
        public string NewPassword { get; set; }
        [Required, MaxLength(50), RegularExpression(pattern:GeneralStaticDataProvider.PasswordPolicyRegEx, ErrorMessage = GeneralStaticDataProvider.PasswordPolicyErrorMessage)]
        public string NewPasswordRepeat { get; set; }
    }

    public class AuthRegisterViewModel
    {
        [MaxLength(100), Required]
        public string FullName { get; set; }
        [MaxLength(200), Required]
        public string Email { get; set; }
        [Required]
        public long MobileNumber { get; set; }
        [MaxLength(50), Required]
        public string DisplayName { get; set; }
        public Country Country { get; set; }
        [MaxLength(50)]
        public string IdentityInformation { get; set; }
        [MaxLength(50)]
        public string OfficialPosition { get; set; }
        [Required, MinLength(1)] 
        public string[] Roles { get; set; }
        [Required, RegularExpression(pattern:GeneralStaticDataProvider.PasswordPolicyRegEx, ErrorMessage = GeneralStaticDataProvider.PasswordPolicyErrorMessage)]
        public string Password { get; set; }
    }

    public class AuthLoginViewModel
    {
        [Required, MaxLength(100)]
        public string Username { get; set; }
        [Required, RegularExpression(pattern:GeneralStaticDataProvider.PasswordPolicyRegEx, ErrorMessage = GeneralStaticDataProvider.PasswordPolicyErrorMessage)]
        public string Password { get; set; }
    }

    public class AuthTokenViewModel
    {
        public string Jwt { get; set; }
    }

    public class AuthUserClaimsViewModel
    {
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string[] Roles { get; set; }
    }
}
