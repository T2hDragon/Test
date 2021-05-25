using System;
using System.Collections.Generic;
using System.Linq;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;

namespace DAL.App.EF.AppDataInit
{
    class AccountManager
    {
        private static UserManager<AppUser> _userManager = default!;
        private static RoleManager<AppRole> _roleManager = default!;

        public AccountManager(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        
        public AppRole CreateRole(string roleName)
        {
            var role = new AppRole();
            role.Name = roleName;
            var result = _roleManager.CreateAsync(role).Result;
            SuccessLog(result, "Can't create role!");


            return role;
        }
        
        public AppUser CreateUser(string firstName,  string lastName, string email, string password)
        {
            var user = new AppUser();
            user.Firstname = firstName;
            user.Lastname = lastName;
            user.Email = email;
            user.UserName = user.Email;
            var result = _userManager.CreateAsync(user, password).Result;
            SuccessLog(result, "Can't create user!");


            return user;
        }
        
        public void AssignUserToRoles(AppUser user,params AppRole[] roles)
        {
            var roleNames = roles.Select(role => role.Name);
            var result = _userManager.AddToRolesAsync(user, roleNames).Result;
            SuccessLog(result, "Can't assign user to role!");
            
            result = _userManager.UpdateAsync(user).Result;
            
            SuccessLog(result, "Can't update user!");

        }

        private void SuccessLog(IdentityResult result, string message)
        {
            if (!result.Succeeded)
            {
                foreach (var identityError in result.Errors)
                {
                    Console.WriteLine($"{message} Error: {identityError.Description}");
                }
            }
        }
            
    }
}