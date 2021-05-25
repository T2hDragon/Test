using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.EF;
using Domain.App;
using Domain.Base;

namespace TestProject.Helpers.Seeding
{
    class EntityManager
    {
        private static AppDbContext _ctx = default!;

        public EntityManager(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        private async Task<LangString> CreateLangString(string valueEn, string valueEe)
        {
            var langString = new LangString
            {
                Translations = new List<Translation>()
            };

            var englishTranslation = new Translation
            {
                Culture = "en",
                Value = valueEn,
                LangString = langString
            };
            await _ctx.Translations.AddAsync(englishTranslation);
            
            var estoniaTranslation = new Translation
            {
                Culture = "et",
                Value = valueEe,
                LangString = langString
            };
            await _ctx.Translations.AddAsync(estoniaTranslation);
            await _ctx.LangStrings.AddAsync(langString);
            await _ctx.SaveChangesAsync();
            return langString;
        }
        
        public  Title CreateTitle(string name)
        {
            var title = new Title
            {
                Name = name
            };
            _ctx.Titles.Add(title);
            var result =  _ctx.SaveChanges() >0;
            if (!result) Console.WriteLine($@"Title {name} couldn't be added!");
            
            return title;
        }
                
        public  Status CreateStatus(string name)
        {
            var status = new Status()
            {
                Name = name
            };
            _ctx.Statuses.Add(status);
            var result =  _ctx.SaveChanges() >0;
            if (!result) Console.WriteLine($@"Status {name} couldn't be added!");
            
            return status;
        }
        
        public Requirement CreateRequirement(string nameEn, string nameEe, string descriptionEn, string descriptionEe, double price, int? amount = null)
        {
            var requirement = new Requirement
            {
                Name = CreateLangString(nameEn, nameEe).Result,
                Description = CreateLangString(descriptionEn, descriptionEe).Result,
                Price = price,
                Amount = amount
            };
            _ctx.Requirements.Add(requirement);
            var result = _ctx.SaveChanges();
            if (!(result > 0)) Console.WriteLine($@"Requirement {nameEn} couldn't be added!");
            
            return requirement;
        }

        
        public  DrivingSchool CreateSchool(string nameEn, string nameEe, string descriptionEn, string descriptionEe, Guid ownerId)
        {
            var school = new DrivingSchool()
            {
                Name = CreateLangString(nameEn, nameEe).Result,
                Description = CreateLangString(descriptionEn, descriptionEe).Result,
                AppUserId = ownerId
            };
             _ctx.DrivingSchools.Add(school);
            var result =  _ctx.SaveChanges() >0;
            if (!result) Console.WriteLine($@"DrivingSchool {nameEn} couldn't be added!");
            
            return school;
        }
        
        public  Course CreateSchoolCourse(string nameEn, string nameEe, string descriptionEn, string descriptionEe, double price, string category, Guid schoolId)
        {
            var course = new Course()
            {
                Name = CreateLangString(nameEn, nameEe).Result,
                Description = CreateLangString(descriptionEn, descriptionEe).Result,
                Price = price,
                Category = category,
                DrivingSchoolId = schoolId
            };
             _ctx.Courses.Add(course);
            var result =  _ctx.SaveChanges() >0;
            if (!result) Console.WriteLine($@"Course {nameEn} couldn't be added!");
            
            return course;
        }
        
        public  CourseRequirement AddRequirementToCourse(string descriptionEn, string descriptionEe, double price, Guid courseId, Guid requirementId, int? amount = null)
        {
            var courseRequirement = new CourseRequirement()
            {
                Description = CreateLangString(descriptionEn, descriptionEe).Result,                Price = price,
                RequirementId = requirementId,
                CourseId = courseId,
                Amount = amount
            };
             _ctx.CourseRequirements.Add(courseRequirement);
            var result =  _ctx.SaveChanges() >0;
            if (!result) Console.WriteLine($@"CourseRequirement with description {descriptionEn} couldn't be added!");
            
            return courseRequirement;
        }
        
        public  Contract CreateContract(Guid appUserId, Guid schoolId, Guid titleId, Guid statusId)
        {
            var contract = new Contract()
            {
                AppUserId = appUserId,
                TitleId = titleId,
                StatusId = statusId,
                DrivingSchoolId = schoolId
            };
             _ctx.Contracts.Add(contract);
            var result =  _ctx.SaveChanges() >0;
            if (!result) Console.WriteLine($@"Contract couldn't be added!");
            
            return contract;
        }
        
        public  ContractCourse AddContractCourse(Guid contractId, Guid courseId, Guid statusId, double hourlyPay)
        {
            var contractCourse = new ContractCourse()
            {
                HourlyPay = hourlyPay,
                ContractId = contractId,
                StatusId = statusId,
                CourseId = courseId,
            };
            _ctx.ContractCourses.Add(contractCourse);
            var result =  _ctx.SaveChanges() >0;
            if (!result) Console.WriteLine($@"ContractCourse couldn't be added!");
            
            return contractCourse;
        }
        
        public Lesson AddLesson(Guid studentContractCourseId, Guid teacherContractCourseId, Guid courseRequirementId, DateTime startTime, double minutes, double price)
        {
            startTime = new DateTime(startTime.Year, startTime.Month, startTime.Day);
            startTime.AddMinutes(startTime.Minute);
            startTime.AddHours(startTime.Hour);
            var lesson = new Lesson
            {
                Start = startTime,
                End = startTime.AddMinutes(minutes),
                CourseRequirementId = courseRequirementId
            };


            _ctx.Lessons.Add(lesson);
            
            var studentParticipant = new LessonParticipant
            {
                ContractCourseId = studentContractCourseId,
                Start = startTime,
                End = startTime.AddMinutes(minutes),
                Price = price,
                Lesson = lesson
            };
            var teacherParticipant = new LessonParticipant
            {
                ContractCourseId = teacherContractCourseId,
                Start = startTime,
                End = startTime.AddMinutes(minutes),
                Lesson = lesson
            };

            _ctx.LessonParticipants.AddRange(studentParticipant, teacherParticipant);
            
            var result =  _ctx.SaveChanges() >0;
            if (!result) Console.WriteLine($@"ContractCourse couldn't be added!");
            
            return lesson;
        }
    }
}