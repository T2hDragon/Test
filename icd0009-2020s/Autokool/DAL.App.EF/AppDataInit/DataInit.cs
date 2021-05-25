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

            
            // Values
            var student1 = ctx.AppUsers.FirstOrDefaultAsync(user => user.UserName == "student1").Result;
            var student2 = ctx.AppUsers.FirstOrDefaultAsync(user => user.UserName == "student2").Result;
            var student3 = ctx.AppUsers.FirstOrDefaultAsync(user => user.UserName == "student3").Result;
            var owner1 = ctx.AppUsers.FirstOrDefaultAsync(user => user.UserName == "owner1").Result;
            var owner2 = ctx.AppUsers.FirstOrDefaultAsync(user => user.UserName == "owner2").Result;
            var teacher1 = ctx.AppUsers.FirstOrDefaultAsync(user => user.UserName == "teacher1").Result;
            var teacher2 = ctx.AppUsers.FirstOrDefaultAsync(user => user.UserName == "teacher2").Result;
            var teacher3 = ctx.AppUsers.FirstOrDefaultAsync(user => user.UserName == "teacher3").Result;
            
            // Requirements
            var drivingLessonsRequirement =
                entityManager.CreateRequirement("Driving Lessons", "Sõidutunnid","Course Driving Lessons description", "Sõidutundide kirjeldus", 20, 32);
            var drivingExamRequirement =
                entityManager.CreateRequirement("Driving Exam", "Sõidueksam","Course Exam description", "Sõidueksami kirjeldus", 20);
            var theoryRequirement =
                entityManager.CreateRequirement("Theory", "Teooria", "Theory Lessons description", "Teooria tundide kirjeldus", 120);
            var slipperyRideRequirement =
                entityManager.CreateRequirement("Slippery Ride", "Libedasõit","Slippery Ride Lessons description", "Libedasõidu kirjeldus", 140);
            var darkRideRequirement =
                entityManager.CreateRequirement("Dark Ride", "Pimedasõit" ,"Dark Ride Lessons description", "Pimedasõidu kirjeldus" ,40);

            
            // Statuses
            var invitedStatus = entityManager.CreateStatus(Statuses.Invited);
            var activeStatus = entityManager.CreateStatus(Statuses.Active);
            var inactiveStatus = entityManager.CreateStatus(Statuses.Inactive);
            
            // Titles
            var studentTitle = entityManager.CreateTitle(Titles.Student);
            var teacherTitle = entityManager.CreateTitle(Titles.Teacher);
            var ownerTitle = entityManager.CreateTitle(Titles.Owner);
            
            // Schools
            var school1 = entityManager.CreateSchool(
                "Aire Tammik Drivingschool",
                "Aire Tammik Autokool",
                "Rapla Drivingschool",
                "Rapla Autokool",

                owner1.Id);
            var school2 = entityManager.CreateSchool(
                "Drivingschool OÜ",
                "Sõidkool OÜ",
                "Rapla Drivingschool",
                "Rapla Autokool",

                owner2.Id);
            
            // School Courses
            var school1BCourse =
                entityManager.CreateSchoolCourse(
                    "B-Category", 
                    "B-Kategooria", 
                    "Worlds best B-Cat course", 
                    "Maailma parim B-kategooria kursus", 
                    420, 
                    "B",
                    school1.Id);
            
            var school1ACourse =
                entityManager.CreateSchoolCourse(
                    "A-Category", 
                    "A-Kategooria", 
                    "Worlds best A-Cat course", 
                    "Maailma parim A-kategooria kursus", 
                    690, 
                    "A",
                    school1.Id);

            // School Course Requirements
            var school1BCourseDrivingRequirement =
                entityManager.AddRequirementToCourse(
                    "Course Driving Lessons description", "Sõidutundide kirjeldus", 
                    drivingLessonsRequirement.Price, 
                    school1BCourse.Id,
                    drivingLessonsRequirement.Id,
                    drivingLessonsRequirement.Amount);
            
            var school1BCourseBlindDrivingRequirement =
                entityManager.AddRequirementToCourse(
                    "Drive blindfolded", 
                    "Pimesilmi sõitmine", 
                    42, 
                    school1BCourse.Id,
                    darkRideRequirement.Id);
            
            var school1BCourseDrivingExamRequirement =
                entityManager.AddRequirementToCourse(
                    "Worlds hardest exam", 
                    "Maailma keerulisem eksam", 
                    42, 
                    school1BCourse.Id,
                    drivingExamRequirement.Id);
            
            var school1ACourseDrivingExamRequirement =
                entityManager.AddRequirementToCourse(
                    "Worlds hardest exam", 
                    "Maailma keerulisem eksam", 
                    42, 
                    school1ACourse.Id,
                    drivingExamRequirement.Id);

            // Add School Contracts
            var teacher1School1TeacherContract = entityManager.CreateContract(
                teacher1.Id,
                school1.Id,
                teacherTitle.Id,
                activeStatus.Id);

            var teacher1School1StudentContract = entityManager.CreateContract(
                teacher1.Id,
                school1.Id,
                studentTitle.Id,
                invitedStatus.Id);
            
            var teacher1School2TeacherContract = entityManager.CreateContract(
                teacher1.Id,
                school2.Id,
                teacherTitle.Id,
                invitedStatus.Id);
            
            var teacher1School2StudentContract = entityManager.CreateContract(
                teacher1.Id,
                school2.Id,
                studentTitle.Id,
                activeStatus.Id);

            var teacher2School1TeacherContract = entityManager.CreateContract(
                teacher2.Id,
                school1.Id,
                teacherTitle.Id,
                activeStatus.Id);
            
            var teacher3School1TeacherContract = entityManager.CreateContract(
                teacher2.Id,
                school1.Id,
                teacherTitle.Id,
                invitedStatus.Id);
            var student1School1StudentContract = entityManager.CreateContract(
                student1.Id,
                school1.Id,
                studentTitle.Id,
                activeStatus.Id);
            var student2School1StudentContract = entityManager.CreateContract(
                student2.Id,
                school1.Id,
                studentTitle.Id,
                activeStatus.Id);

            // Add ContractCourse
            var teacher1School1BCourseTeacherContract = entityManager.AddContractCourse(
                teacher1School1TeacherContract.Id,
                school1BCourse.Id,
                activeStatus.Id,
                15);
            
            var teacher2School1BCourseTeacherContract = entityManager.AddContractCourse(
                teacher2School1TeacherContract.Id,
                school1BCourse.Id,
                activeStatus.Id,
                15);
            
            var student1School1BCourseStudentContract = entityManager.AddContractCourse(
                student1School1StudentContract.Id,
                school1BCourse.Id,
                activeStatus.Id,
                0);
            
            var student1School1ACourseStudentContract = entityManager.AddContractCourse(
                student1School1StudentContract.Id,
                school1ACourse.Id,
                activeStatus.Id,
                0);
            
            var student2School1BCourseStudentContract = entityManager.AddContractCourse(
                student2School1StudentContract.Id,
                school1BCourse.Id,
                activeStatus.Id,
                0);
            
            // Add Course checks
            var student1School1BCourseBlindDrivingLesson = entityManager.AddLesson(
                student1School1BCourseStudentContract.Id,
                teacher2School1BCourseTeacherContract.Id,
                school1BCourseBlindDrivingRequirement.Id,
                DateTime.Now.AddDays(-1).AddHours(-2),
                0,
                school1BCourseBlindDrivingRequirement.Price
            );
            
            // Add Driving Lessons
            var student2School1BCourseDrivingLesson1 = entityManager.AddLesson(
                student2School1BCourseStudentContract.Id,
                teacher2School1BCourseTeacherContract.Id,
                school1BCourseDrivingRequirement.Id,
                DateTime.Now,
                90,
                school1BCourseDrivingRequirement.Price
            );

            var student1School1BCourseDrivingLesson1 = entityManager.AddLesson(
                student1School1BCourseStudentContract.Id,
                teacher1School1BCourseTeacherContract.Id,
                school1BCourseDrivingRequirement.Id,
                currentTime.AddDays(-27),
                90,
                school1BCourseDrivingRequirement.Price
            );
            var student1School1BCourseDrivingLesson2 = entityManager.AddLesson(
                student1School1BCourseStudentContract.Id,
                teacher1School1BCourseTeacherContract.Id,
                school1BCourseDrivingRequirement.Id,
                currentTime.AddDays(-17),
                90,
                school1BCourseDrivingRequirement.Price
            );
            var student1School1BCourseDrivingLesson3 = entityManager.AddLesson(
                student1School1BCourseStudentContract.Id,
                teacher1School1BCourseTeacherContract.Id,
                school1BCourseDrivingRequirement.Id,
                currentTime.AddDays(-14),
                90,
                school1BCourseDrivingRequirement.Price
            );
            var student1School1BCourseDrivingLesson4 = entityManager.AddLesson(
                student1School1BCourseStudentContract.Id,
                teacher1School1BCourseTeacherContract.Id,
                school1BCourseDrivingRequirement.Id,
                currentTime.AddDays(-10),
                90,
                school1BCourseDrivingRequirement.Price
            );
            var student1School1BCourseDrivingLesson5 = entityManager.AddLesson(
                student1School1BCourseStudentContract.Id,
                teacher1School1BCourseTeacherContract.Id,
                school1BCourseDrivingRequirement.Id,
                currentTime.AddDays(-8),
                90,
                school1BCourseDrivingRequirement.Price
            );
            var student1School1BCourseDrivingLesson6 = entityManager.AddLesson(
                student1School1BCourseStudentContract.Id,
                teacher1School1BCourseTeacherContract.Id,
                school1BCourseDrivingRequirement.Id,
                currentTime.AddDays(-5),
                90,
                school1BCourseDrivingRequirement.Price
            );
            var student1School1BCourseDrivingLesson7 = entityManager.AddLesson(
                student1School1BCourseStudentContract.Id,
                teacher1School1BCourseTeacherContract.Id,
                school1BCourseDrivingRequirement.Id,
                currentTime.AddDays(-2),
                90,
                school1BCourseDrivingRequirement.Price
            );
            var student2School1BCourseDrivingLesson8 = entityManager.AddLesson(
                student2School1BCourseStudentContract.Id,
                teacher1School1BCourseTeacherContract.Id,
                school1BCourseDrivingRequirement.Id,
                currentTime.AddDays(-1),
                90,
                school1BCourseDrivingRequirement.Price
            );
            var student1School1BCourseDrivingLesson9 = entityManager.AddLesson(
                student1School1BCourseStudentContract.Id,
                teacher1School1BCourseTeacherContract.Id,
                school1BCourseDrivingRequirement.Id,
                currentTime,
                90,
                school1BCourseDrivingRequirement.Price
            );
            
            var student1School1BCourseDrivingLesson10 = entityManager.AddLesson(
                student1School1BCourseStudentContract.Id,
                teacher1School1BCourseTeacherContract.Id,
                school1BCourseDrivingRequirement.Id,
                currentTime.AddHours(1).AddMinutes(30),
                90,
                school1BCourseDrivingRequirement.Price
            );
            
            var student1School1BCourseDrivingLesson11 = entityManager.AddLesson(
                student1School1BCourseStudentContract.Id,
                teacher1School1BCourseTeacherContract.Id,
                school1BCourseDrivingRequirement.Id,
                currentTime.AddHours(3).AddMinutes(30),
                60,
                school1BCourseDrivingRequirement.Price
            );
            var student1School1BCourseDrivingLesson12 = entityManager.AddLesson(
                student1School1BCourseStudentContract.Id,
                teacher1School1BCourseTeacherContract.Id,
                school1BCourseDrivingRequirement.Id,
                currentTime.AddDays(1),
                90,
                school1BCourseDrivingRequirement.Price
            );
            var student1School1BCourseDrivingLesson13 = entityManager.AddLesson(
                student1School1BCourseStudentContract.Id,
                teacher1School1BCourseTeacherContract.Id,
                school1BCourseDrivingRequirement.Id,
                currentTime.AddDays(7),
                90,
                school1BCourseDrivingRequirement.Price
            );
            
        }
        

        public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            var accountManager = new AccountManager(userManager, roleManager);
            var adminRole = accountManager.CreateRole(AppRoles.Administrator, "Admin");
            var teacherRole = accountManager.CreateRole(AppRoles.Teacher, "Teacher");
            var studentRole = accountManager.CreateRole(AppRoles.Student, "Student");
            var ownerRole = accountManager.CreateRole(AppRoles.Owner, "School owner");

            var developerUser = accountManager.CreateUser( "admin","Karmo", "Alteberg", "kaalte@ttu.ee", "DucksG0Quack!");

            var owner1User = accountManager.CreateUser( "owner1","Aire", "Tammik", "aireTammik@gmail.com", "ValgeAknaraam!1");
            var owner2User = accountManager.CreateUser( "owner2","Aire2", "Tammik2", "aireTammik2@gmail.com", "ValgeAknaraam!1");
            var teacherUser1 = accountManager.CreateUser( "teacher1","Jüri", "Mägi", "juMagi@gmail.com", "SafePassw0rd!23");
            var teacherUser2 = accountManager.CreateUser( "teacher2","Caspar", "Rudolf", "teehee@gmail.com", "SafePassw0rd!23");
            var teacherUser3 = accountManager.CreateUser( "teacher3","Rolfar", "Guesse", "nigahiga@gmail.com", "SafePassw0rd!23");

            var student1User = accountManager.CreateUser( "student1","Tudeng", "Pähkel", "mihkelp2hkel@mail.ru", "SaaremaaKunn!23");
            var student2User = accountManager.CreateUser( "student2","Mihkel", "Roofti", "mihkelp2hkel@mail.ru", "SaaremaaKunn!23");
            var student3User = accountManager.CreateUser( "student3","Beth", "Niguliste", "mihkelp2hkel@mail.ru", "SaaremaaKunn!23");

            accountManager.AssignUserToRoles(developerUser, adminRole);
            accountManager.AssignUserToRoles(owner1User, ownerRole);
            accountManager.AssignUserToRoles(owner2User, ownerRole);
            accountManager.AssignUserToRoles(teacherUser1);
            accountManager.AssignUserToRoles(teacherUser2);
            accountManager.AssignUserToRoles(teacherUser3);
            accountManager.AssignUserToRoles(student1User);
            accountManager.AssignUserToRoles(student2User);
            accountManager.AssignUserToRoles(student3User);

        }


        

    }
}