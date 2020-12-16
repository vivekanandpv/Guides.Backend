using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Guides.Backend.Domain;

namespace Guides.Backend.Repositories.Auth
{
    public interface IAuthRepository
    {
        IQueryable<User> GetUsers();
        Task<User> GetUserById(int id);
        Task<User> GetUserByMobileNumber(long mobileNumber);
        Task<User> GetUserByEmail(string email);
        Task<bool> UserExists(long mobileNumber);
        Task<bool> UserExists(string email);
        Task<bool> RoleExists(string role);
        IQueryable<Role> GetRoles();
        Task<Role> GetRoleByName(string role);
        Task<User> AddUser(User user);
        Task<User> UpdateUser(User user);
        string[] GetRolesForUser(User user);
    }
}
