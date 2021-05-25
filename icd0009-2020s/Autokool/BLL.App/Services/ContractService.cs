using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Microsoft.EntityFrameworkCore;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace BLL.App.Services
{
    public class ContractService :
        BaseEntityService<IAppUnitOfWork, IContractRepository, BLLAppDTO.Contract, DALAppDTO.Contract>, IContractService
    {
        public ContractService(IAppUnitOfWork serviceUow, IContractRepository serviceRepository, IMapper mapper) : base(
            serviceUow, serviceRepository, new ContractMapper(mapper))
        {
        }

        public async Task<IEnumerable<BLLAppDTO.Contract?>> GetContractsBySchool(Guid schoolId, string? title = null, string? status = null, bool noTracking = true)
        {
            return (await ServiceRepository.GetContractsBySchool(schoolId, title, status)).Select(x =>
                Mapper.Map(x))!;
        }


        public async Task<IEnumerable<BLLAppDTO.Teacher>> GetSchoolTeachers(Guid schoolId, bool noTracing = true)
        {
            var teacherContracts =
                (await ServiceRepository.GetContractsBySchool(schoolId,
                    Domain.App.Constants.Titles.Teacher, Domain.App.Constants.Statuses.Active, noTracing)).Select(x => Mapper.Map(x)).ToList();
            if (teacherContracts == null) throw new ArgumentNullException(nameof(teacherContracts));
            var teachers = teacherContracts.Select(teacher =>
            {
                var courses =
                    (teacher!.ContractCourses ?? throw new InvalidOperationException()).Select(cc => cc.Course)!;
                return new BLLAppDTO.Teacher()
                {
                    ContractId = teacher.Id,
                    ContractCourses = teacher.ContractCourses,
                    CoursesNameRep = String.Join(", ", courses.Select(course => course!.Name)),
                    Courses = courses!,
                    Email = teacher.AppUser!.Email,
                    Name = teacher.AppUser.FullName
                };
            });
            return teachers!;
        }

        public async Task<BLLAppDTO.Teacher> GetTeacher(Guid contractId, bool noTracing = true)
        {
            var teacherContract = Mapper.Map(await ServiceRepository.FirstOrDefaultWithCoursesAsync(contractId));
            var courses = teacherContract!.ContractCourses!.Select(course => course.Course).ToList();
            var teacher = new BLLAppDTO.Teacher()
            {
                ContractCourses = teacherContract.ContractCourses!.ToList(),
                ContractId = teacherContract.Id,
                Courses = courses!,
                CoursesNameRep = String.Join(", ", courses.Select(course => course!.Name)),
                Email = teacherContract.AppUser!.Email,
                Name = teacherContract.AppUser.FullName
            };
            return teacher;
        }


        public async Task<BLLAppDTO.PeriodReport> GetContractPeriodReport(Guid contractId, DateTime searchStart,
            DateTime searchEnd, bool noTracing = true)
        {
            var lessonParticipations =
                await ServiceUow.LessonParticipants.GetContractDrivingLessonPartitions(contractId, searchStart, searchEnd);
            double totalHours = 0;
            double totalSalary = 0;
            var drivingLessons = new List<BLLAppDTO.DrivingLesson>();

            foreach (var lessonParticipation in lessonParticipations)
            {
                var teachers = await ServiceRepository.GetLessonContractsByTitle(lessonParticipation.LessonId!.Value,
                    Domain.App.Constants.Titles.Teacher);
                var students = await ServiceRepository.GetLessonContractsByTitle(lessonParticipation.LessonId.Value,
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
                var lessonHours = (lessonParticipation.End - lessonParticipation.Start)!.Value.TotalMinutes / 60;
                totalHours += lessonHours;
                totalSalary += lessonParticipation.ContractCourse.HourlyPay*lessonHours;
            }

            var periodReport = new BLLAppDTO.PeriodReport
            {
                Start = searchStart,
                End = searchEnd,
                DrivingLessons = drivingLessons,
                TotalHours = totalHours,
                TotalSalary = totalSalary,
            };
            return periodReport;
        }


        public async Task<IEnumerable<BLLAppDTO.Contract>> GetSchoolContracts(Guid schoolId, string username = "", string fullname = "", string? title = null,
            string? status = null)
        {
            return (await ServiceRepository.GetSchoolContracts(schoolId, username, fullname, title)).Select(x => Mapper.Map(x)!);
        }

        public async Task<IEnumerable<BLLAppDTO.Contract>> GetContractsByAppUser(Guid AppUserId)
        {
            return (await ServiceRepository.GetContractsByAppUser(AppUserId)).Select(x => Mapper.Map(x)!);
        }
        public async Task<string> GetContractorName(Guid contractId, bool noTracing = true)
        {
            return await ServiceRepository.GetContractorName(contractId, noTracing);
        }
        public async Task<IEnumerable<BLLAppDTO.Contract>> GetLessonContractsByTitle(Guid lessonId, string title)
        {
            return (await ServiceRepository.GetLessonContractsByTitle(lessonId, title)).Select(x => Mapper.Map(x)!);
        }

        public async Task<bool> IsFree(Guid contractId, DateTime startingDate, DateTime endingDate)
        {
            return await ServiceRepository.IsFree(contractId, startingDate, endingDate);
        }

        public async Task<BLLAppDTO.Contract?> GetSchoolContractWithTitle(Guid appUserId, Guid schoolId, string title, string status)
        {
            return Mapper.Map(await ServiceRepository.GetSchoolContractWithTitle(appUserId, schoolId, title, status));
        }


        public async Task<BLLAppDTO.Contract?> GetContractByUsername(string username, Guid schoolId, string title, string status)
        {
            return Mapper.Map(await ServiceRepository.GetContractByUsername(username, schoolId, title, status));
        }

        public async Task<IEnumerable<BLLAppDTO.Contract>> GetSchoolContractsByName(string name, Guid schoolId, string? title = null, string? status = null)
        {
            return (await ServiceRepository.GetSchoolContractsByName(name, schoolId, title, status)).Select(x => Mapper.Map(x)!);
        }
        public async Task<IEnumerable<BLLAppDTO.Contract>> GetSchoolContractsByUsername(string username, Guid schoolId, string? title = null, string? status = null)
        {
            return (await ServiceRepository.GetSchoolContractsByUsername(username, schoolId, title, status)).Select(x => Mapper.Map(x)!);
        }
    }
}