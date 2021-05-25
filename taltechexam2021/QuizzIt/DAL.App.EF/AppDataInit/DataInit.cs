using System;
using System.Collections;
using System.Linq;
using Domain.App.Identity;
using Domain.App.Constants;
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
            var entityManager = new EntityManager(ctx);
            var currentTime = DateTime.Now;
        }
        

        public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            var accountManager = new AccountManager(userManager, roleManager);
            var adminRole = accountManager.CreateRole(AppRoles.Administrator, "Administrator");
            var quizCreator = accountManager.CreateRole(AppRoles.QuizCreator,"Quiz Creator");

            var developerUser = accountManager.CreateUser( "admin","Karmo", "Alteberg", "kaalte@ttu.ee", "DucksG0Quack!");

            var quizCreator1 = accountManager.CreateUser( "quizCreator1","Aire", "Tammik", "aireTammik@gmail.com", "ValgeAknaraam!1");
            var quizCreator2 = accountManager.CreateUser( "quizCreator2","NAna", "panawr", "aireTammik2@gmail.com", "ValgeAknaraam!1");
            var participant1 = accountManager.CreateUser( "participant1","Jüri", "Mägi", "juMagi@gmail.com", "SafePassw0rd!23");
            var participant2 = accountManager.CreateUser( "participant2","Caspar", "Rudolf", "teehee@gmail.com", "SafePassw0rd!23");
            var participant3 = accountManager.CreateUser( "participant3","Toom", "Room", "teehee2@gmail.com", "SafePassw0rd!23");
            var participant4 = accountManager.CreateUser( "participant4","Ralh", "Tuyrp", "teehee3@gmail.com", "SafePassw0rd!23");

            accountManager.AssignUserToRoles(developerUser, adminRole);
            accountManager.AssignUserToRoles(quizCreator1, quizCreator);
            accountManager.AssignUserToRoles(quizCreator2, quizCreator);

        }


        

    }
}