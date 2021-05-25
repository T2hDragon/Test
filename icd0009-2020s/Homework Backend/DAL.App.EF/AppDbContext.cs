using System;
using System.Linq;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        
        public DbSet<Person> Persons { get; set; } = default!;
        
        public DbSet<Contact> Contacts { get; set; } = default!;
        public DbSet<ContactType> ContactTypes { get; set; } = default!;
        public DbSet<Contract> Contracts { get; set; } = default!;
        public DbSet<ContractCourse> ContractCourses { get; set; } = default!;
        public DbSet<Course> Courses { get; set; } = default!;
        public DbSet<CourseRequirement> CourseRequirements { get; set; } = default!;
        public DbSet<DrivingSchool> DrivingSchools { get; set; } = default!;
        public DbSet<Lesson> Lessons { get; set; } = default!;
        public DbSet<LessonCourseRequirement> LessonCourseRequirements { get; set; } = default!;
        public DbSet<LessonParticipant> LessonParticipants { get; set; } = default!;
        public DbSet<LessonParticipantConfirmation> LessonParticipantConfirmations { get; set; } = default!;
        public DbSet<LessonParticipantNote> LessonParticipantNotes { get; set; } = default!;
        public DbSet<Requirement> Requirements { get; set; } = default!;
        public DbSet<WorkHour> WorkHours { get; set; } = default!;
        public DbSet<Simple> Simples { get; set; } = default!;
        public DbSet<IntPkThing> IntPkThings { get; set; } = default!;

        
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            // disable cascade delete initially for everything
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }


        }
    }
}