using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.App;
using Domain.App.Identity;
using Domain.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DAL.App.EF
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {        
        public DbSet<Quiz> Quizzes { get; set; } = default!;
        public DbSet<Answer> Answers { get; set; } = default!;
        public DbSet<Question> Questions { get; set; } = default!;
        public DbSet<PickedAnswer> PickedAnswers { get; set; } = default!;
        public DbSet<Participant> Participants { get; set; } = default!;
        public DbSet<LangString> LangStrings { get; set; } = default!;
        public DbSet<Translation> Translations { get; set; } = default!;


        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<Translation>().HasKey(k => new {k.Culture, k.LangStringId});


            // disable cascade delete initially for everything
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            // On delete Cascades
            

            // Remove MadeQuizzes upon AppUser delete
            builder.Entity<AppUser>().HasMany(user => user.MadeQuizzes).WithOne(quiz => quiz.Creator)
                .OnDelete(DeleteBehavior.Cascade);
            
            // Remove PickedAnswers upon Answer delete
            builder.Entity<Answer>().HasMany(user => user.PickedAnswers).WithOne(answer => answer.Answer)
                .OnDelete(DeleteBehavior.Cascade);
            
            // Remove Answer upon Question delete
            builder.Entity<Question>().HasMany(user => user.Answers).WithOne(answer => answer.Question!)
                .OnDelete(DeleteBehavior.Cascade);

            // Remove Question upon Quiz delete
            builder.Entity<Quiz>().HasMany(user => user.Questions).WithOne(answer => answer.Quiz!)
                .OnDelete(DeleteBehavior.Cascade);
            
            // Remove Participant upon Quiz delete
            builder.Entity<Quiz>().HasMany(user => user.Participants).WithOne(answer => answer.Quiz)
                .OnDelete(DeleteBehavior.Cascade);


            // Remove Translations upon LangStrings delete
            builder.Entity<LangString>().HasMany(LangStrings => LangStrings.Translations)
                .WithOne(translation => translation.LangString!)
                .OnDelete(DeleteBehavior.Cascade);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(w => w.Throw(RelationalEventId.MultipleCollectionIncludeWarning));
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        public async void AddUserToRole(Guid userId, string roleName)
        {
            var role = Roles.FirstOrDefaultAsync(role => role.Name == roleName).Result;
            var userRole = new IdentityUserRole<Guid>
            {
                RoleId = role.Id,
                UserId = userId
            };
            await UserRoles.AddAsync(userRole);
            await SaveChangesAsync();
        }

        public async void AddUserToRole(Guid userId, Guid roleId)
        {
            var userRole = new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = userId
            };
            await UserRoles.AddAsync(userRole);
            await SaveChangesAsync();
        }

        public async Task<Domain.App.Identity.AppRole> CreateRoleAsync(string name, string displayName)
        {
            var role = new Domain.App.Identity.AppRole
            {
                Name = name,
                NormalizedName = name.ToUpper(),
                DisplayName = displayName,
                CreatedAt = DateTime.Now
            };
            await Roles.AddAsync(role);
            await SaveChangesAsync();
            return role;
        }

        public AppUser CreateUser(string username, string firstName,  string lastName, string email, string password)
        {
            var user = new AppUser
            {
                UserName = username,
                NormalizedUserName = username.ToUpper(),
                Firstname = firstName,
                Lastname = lastName,
                Email = email,
                EmailConfirmed = true,
                NormalizedEmail = email.ToUpper()
            };
            var entity = Users.Add(user);
            var result = SaveChanges() > 0;
            if (!result) Console.WriteLine($"Can't create user {username}!");
            entity.State = EntityState.Detached;

            return user;
        }

        public async void RemoveUserFromRole(Guid userId, string roleName)
        {
            var user = Users.FirstOrDefaultAsync(user => user.Id == userId).Result;
            var role = Roles.FirstOrDefaultAsync(role => role.Name == "owner").Result;
            var userRole =
                await UserRoles.FirstOrDefaultAsync(
                    userRole => userRole.RoleId == role.Id && user.Id == userRole.UserId);
            var result = UserRoles.Remove(userRole);
            await SaveChangesAsync();
        }
    }
}