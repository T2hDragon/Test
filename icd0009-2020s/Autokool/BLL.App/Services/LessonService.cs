using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace BLL.App.Services
{
    public class LessonService :
        BaseEntityService<IAppUnitOfWork, ILessonRepository, BLLAppDTO.Lesson, DALAppDTO.Lesson>, ILessonService
    {
        public LessonService(IAppUnitOfWork serviceUow, ILessonRepository serviceRepository, IMapper mapper) : base(
            serviceUow, serviceRepository, new LessonMapper(mapper))
        {
        }

        public async Task<BLLAppDTO.DrivingLesson?> GetDrivingLesson(Guid lessonId, bool noTracing = true)
        {
            var lesson =
                await ServiceRepository.FirstOrDefaultAsync(lessonId);
            if (lesson == null)
            {
                return null;
            }

            var courseName = lesson.CourseRequirement.Course!.Name;

            var teachers = await ServiceUow.Contracts.GetLessonContractsByTitle(lessonId,
                Domain.App.Constants.Titles.Teacher);
            var students = await ServiceUow.Contracts.GetLessonContractsByTitle(lessonId,
                Domain.App.Constants.Titles.Student);
            var drivingLesson = new BLLAppDTO.DrivingLesson
            {
                LessonId = lessonId,
                CourseName = courseName,
                Start = (DateTime) lesson.Start!,
                End = (DateTime) lesson.End!,
                Teachers = String.Join(", ", teachers.Select(teacher => teacher.AppUser!.FullName)),
                Students = String.Join(", ", students.Select(student => student.AppUser!.FullName))
            };


            return drivingLesson;
        }

        public async Task<IEnumerable<BLLAppDTO.DrivingLesson>> GetContractDrivingLessons(Guid contractId,
            DateTime startingDate, DateTime endingDate, bool noTracing = true)
        {
            var contractCourses = await ServiceUow.ContractCourses.GetByContract(contractId);
            var lessonParticipations = new List<DALAppDTO.LessonParticipant>();
            foreach (var contractCourse in contractCourses)
            {
                lessonParticipations.AddRange(await ServiceUow.LessonParticipants.GetContractCourseDrivingLessonPartitions(contractCourse.Id, startingDate,
                    endingDate));
            }

            var drivingLessons = new List<BLLAppDTO.DrivingLesson>();

            foreach (var lessonParticipation in lessonParticipations)
            {
                if (lessonParticipation.LessonId == null)
                {
                    continue;
                }

                var teachers = await ServiceUow.Contracts.GetLessonContractsByTitle(
                    (Guid) lessonParticipation.LessonId.Value,
                    Domain.App.Constants.Titles.Teacher);
                var students = await ServiceUow.Contracts.GetLessonContractsByTitle(
                    (Guid) lessonParticipation.LessonId.Value,
                    Domain.App.Constants.Titles.Student);
                var drivingLesson = new BLLAppDTO.DrivingLesson
                {
                    LessonId = lessonParticipation.LessonId.Value,
                    CourseName = lessonParticipation.ContractCourse!.Course!.Name,
                    Start = lessonParticipation.Start,
                    End = (DateTime) lessonParticipation.End!,
                    Teachers = String.Join(", ", teachers.Select(teacher => teacher.AppUser!.FullName)),
                    Students = String.Join(", ", students.Select(student => student.AppUser!.FullName))
                };
                drivingLessons.Add(drivingLesson);
            }

            return drivingLessons;
        }

        
        public async Task<IEnumerable<BLLAppDTO.DrivingLesson>> GetContractCourseDrivingLessons(Guid contractCourseId,
            DateTime startingDate, DateTime endingDate, bool noTracing = true)
        {
            var lessonParticipations =
                await ServiceUow.LessonParticipants.GetContractCourseDrivingLessonPartitions(contractCourseId, startingDate,
                    endingDate);

            var drivingLessons = new List<BLLAppDTO.DrivingLesson>();

            foreach (var lessonParticipation in lessonParticipations)
            {
                if (lessonParticipation.LessonId == null) continue;
                var teachers = await ServiceUow.Contracts.GetLessonContractsByTitle(
                    (Guid) lessonParticipation.LessonId.Value,
                    Domain.App.Constants.Titles.Teacher);
                var students = await ServiceUow.Contracts.GetLessonContractsByTitle(
                    (Guid) lessonParticipation.LessonId.Value,
                    Domain.App.Constants.Titles.Student);
                var drivingLesson = new BLLAppDTO.DrivingLesson
                {
                    LessonId = lessonParticipation.LessonId.Value,
                    CourseName = lessonParticipation.ContractCourse!.Course!.Name,
                    Start = lessonParticipation.Start,
                    End = (DateTime) lessonParticipation.End!,
                    Teachers = String.Join(", ", teachers.Select(teacher => teacher.AppUser!.FullName)),
                    Students = String.Join(", ", students.Select(student => student.AppUser!.FullName))
                };
                drivingLessons.Add(drivingLesson);
            }

            return drivingLessons;
        }

        public async Task DeleteContractCourseRequirementLessons(Guid contractCourseId, Guid courseRequirementId)
        {
            await ServiceRepository.DeleteContractCourseRequirementLessons(contractCourseId, courseRequirementId);
        }
    }
}