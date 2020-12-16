using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Guides.Backend.Data;
using Guides.Backend.Domain;
using Microsoft.EntityFrameworkCore;

namespace Guides.Backend.Repositories.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly GuidesContext _context;

        public AuthRepository(GuidesContext context)
        {
            _context = context;
        }
        public IQueryable<User> GetUsers()
        {
            return this._context.Users;
        }

        public async Task<User> GetUserById(int id)
        {
            return await this._context
                .Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetUserByMobileNumber(long mobileNumber)
        {
            return await this._context
                .Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.MobileNumber == mobileNumber);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await this._context
                .Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> UserExists(long mobileNumber)
        {
            return await this._context.Users.AnyAsync(u => u.MobileNumber == mobileNumber);
        }

        public async Task<bool> UserExists(string email)
        {
            return await this._context.Users.AnyAsync(u => u.Email == email);

        }

        public async Task<bool> RoleExists(string role)
        {
            return await this._context.Roles.AnyAsync(r => r.Name == role);

        }

        public IQueryable<Role> GetRoles()
        {
            return this._context.Roles;
        }

        public async Task<Role> GetRoleByName(string role)
        {
            return await this._context.Roles.FirstOrDefaultAsync(r => r.Name == role);
        }

        public async Task<User> AddUser(User user)
        {
            await this._context.AddAsync(user);
            await this._context.SaveChangesAsync();

            return user;
        }

        public async Task<User> UpdateUser(User user)
        {
            this._context.Entry(user).State = EntityState.Modified;
            await this._context.SaveChangesAsync();

            return user;
        }

        public string[] GetRolesForUser(User user)
        {
            return user.UserRoles.Select(ur => ur.Role.Name).ToArray();
        }
    }
}
