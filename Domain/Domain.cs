using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Guides.Backend.Domain
{
    public class User
    {
        public int Id { get; set; }
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
        [Required]
        public DateTime CreatedOn { get; set; }
        
        //  Auth
        public DateTime? LockedOn { get; set; }
        public DateTime? AdminResetOn { get; set; }
        public DateTime? LoginResetOn { get; set; }
        public DateTime? AdminBlockOn { get; set; }
        public DateTime? LoginBlockOn { get; set; }
        public DateTime? PasswordResetOn { get; set; }
        public bool IsAdminLocked { get; set; }
        public bool IsLoginLocked { get; set; }
        public string ResetKey { get; set; }
        public DateTime? ResetKeyExpiresOn { get; set; }
        public int FailedAttempts { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }

    public class Role
    {
        public int Id { get; set; }
        [MaxLength(50), Required]
        public string Name { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }

    public class UserRole
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
