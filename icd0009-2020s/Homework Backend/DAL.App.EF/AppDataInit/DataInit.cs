using System.Collections;
using System.Linq;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.AppDataInit
{
    public class DataInit
    {
        public static void DropDatabase(AppDbContext ctx)
        {
            ctx.Database.EnsureDeleted();
        }

        public static void MigrateDatabase(AppDbContext ctx)
        {
            ctx.Database.Migrate();
        }
        
        public static void SeedAppData(AppDbContext ctx)
        {
            ctx.SaveChanges();
        }
        

        public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            var accountManager = new AccountManager(userManager, roleManager);
            var adminRole = accountManager.CreateRole("Admin");
            var userRole = accountManager.CreateRole("User");

            var developerUser = accountManager.CreateUser( "Karmo", "Alteberg", "kaalte@ttu.ee", "DucksG0Quack!");

            accountManager.AssignUserToRoles(developerUser, userRole, adminRole);

        }


        

    }
}