using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WEBPEPE.Domain.Entities;
using WEBPEPE.Domain.Interfaces;
using WEBPEPE.Infrastructure.Identity;
using WEBPEPE.Infrastructure.Mapping;

namespace WEBPEPE.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUser

    {
        public readonly UserManager<AppIdentityUser> _userManager;
       
        public UserRepository(UserManager<AppIdentityUser> userManager)
        {

            _userManager = userManager;
            
        }



        public async Task<User> AddToRolesAsync(User user, string roleName)
        {
            var userDb = await _userManager.FindByEmailAsync(user.Email);

            if (userDb != null)
            {
                await _userManager.AddToRoleAsync(userDb, roleName);
            }

            return user;
        }

        public async Task<bool> CheckPasswordAsync(string userId, string password)
        {
            var user = await _userManager.FindByIdAsync(userId);


            return user != null && await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<User> CreateUser(User user)
        {
            var Result = await _userManager.CreateAsync(user.ToIdentityUser(), user.Password);

            if (Result.Succeeded)

            {
                var newUser = await _userManager.FindByEmailAsync(user.Email);
                user.Id = new Guid(newUser.Id);




                return user;

            }

            return null;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            return user?.ToDomainUser();
        }

        public async Task<List<string>> GetUserRoles(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }

        public async Task<bool> UserExists(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }
    }
}
