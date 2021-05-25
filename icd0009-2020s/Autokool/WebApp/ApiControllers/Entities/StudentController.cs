using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App.DTO;
using Contracts.BLL.App;
using Extensions.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicAPI.DTO.v1;
using PublicAPI.DTO.v1.Entities;
using PublicAPI.DTO.v1.Mappers;
using StudentCourseDTO = PublicAPI.DTO.v1.Entities.StudentCourse;
using Resource = Resources.Errors;

namespace WebApp.ApiControllers.Entities
{
    /// <summary>
    ///     Controller with functionality for managing BLL Student objects
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private StudentCourseMapper _studentCourseMapper;


        /// <summary>
        ///     Constructor for Student controller
        /// </summary>
        /// <param name="bll">Business Logic Layer</param>
        /// <param name="mapper">Mapper</param>
        public StudentsController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _studentCourseMapper = new StudentCourseMapper(mapper);
        }

        /// <summary>
        /// Get contract Courses
        /// </summary>
        /// <param name="contractId"> Searching contract ID </param>
        /// <returns>List of Contract Courses</returns>
        [HttpGet("{contractId}")]
        [ActionName("Courses")]
        [ProducesResponseType(typeof(IEnumerable<StudentCourseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<StudentCourseDTO>>> GetCourses(Guid contractId)
        {
            var contract = await _bll.Contracts.FirstOrDefaultAsync(contractId);
            var contractCourses =
                await _bll.ContractCourses.GetByContract(contractId);
            
            if (await _bll.DrivingSchools.HasUserWithTitle(User.GetUserId()!.Value, contract!.DrivingSchoolId!.Value,
                Domain.App.Constants.Titles.Teacher))
            {
                var teacherContract = await _bll.Contracts.GetSchoolContractWithTitle(User.GetUserId()!.Value,
                    contract.DrivingSchoolId!.Value,
                    Domain.App.Constants.Titles.Teacher, Domain.App.Constants.Statuses.Active);
                var notPermittedCourses = (await _bll.Courses.GetContractMissingCourses(teacherContract!.Id)).ToList();
                var permittedCheckedContractCourses = new List<BLL.App.DTO.ContractCourse>();
                foreach (var contractCourse in contractCourses)
                {
                    if (notPermittedCourses!.Any(notPermittedCourse => notPermittedCourse!.Id == contractCourse.CourseId)) continue;
                    permittedCheckedContractCourses.Add(contractCourse);
                }
                contractCourses = permittedCheckedContractCourses;
            }
            
            return Ok(contractCourses.Select(x => _studentCourseMapper.Map(x)));
        }

        /// <summary>
        ///     Get Course by course ID.
        /// </summary>
        /// <param name="courseId">Course ID</param>
        /// <returns> Searched Contract object</returns>
        [HttpGet("{courseId}")]
        [ActionName("Course")]
        [ProducesResponseType(typeof(IEnumerable<StudentCourseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<StudentCourseDTO>>> GetCourse(Guid courseId)
        {
            var contractCourses =
                await _bll.ContractCourses.FirstOrDefaultAsync(courseId);
            return Ok(_studentCourseMapper.Map(contractCourses));
        }

        /// <summary>
        ///  Get student contract course Report
        /// </summary>
        /// <param name="studentCourseId">ID of contract course that belongs to a student Title</param>
        /// <returns>Student Course Report</returns>
        [HttpGet("{studentCourseId}")]
        [ActionName("CourseReport")]
        [ProducesResponseType(typeof(StudentCourseReport), 200)]
        [ProducesResponseType(typeof(Message), 404)]
        public async Task<ActionResult<StudentCourseReport>> GetStudentReport(Guid studentCourseId)
        {
            var studentCourse = await _bll.ContractCourses.FirstOrDefaultAsync(studentCourseId);
            if (studentCourse == null)
            {
                return NotFound(new Message($"Student course with id of {studentCourseId} was not found"));
            }

            if (studentCourse.CourseId == null)
            {
                return NotFound(new Message($"Student contract {studentCourseId} does not have a course"));
            }

            var finishedRequirements =
                await _bll.CourseRequirements.GetAllCheckmarkableByContractCourse(studentCourseId, true);
            var unFinishedRequirements =
                await _bll.CourseRequirements.GetAllCheckmarkableByContractCourse(studentCourseId, false);
            var drivingRequirement =
                await _bll.CourseRequirements.GetDrivingRequirement(
                    ((await _bll.ContractCourses.FirstOrDefaultAsync(studentCourseId))!).CourseId!.Value);

            var checkmarkProgress = new List<RequirementProgress>();
            foreach (var finishedRequirement in finishedRequirements)
            {
                checkmarkProgress.Add(
                    new RequirementProgress
                    {
                        Id = finishedRequirement.Id,
                        RequirementName = finishedRequirement.Requirement!.Name,
                        IsCompleted = true
                    }
                );
            }

            foreach (var unFinishedRequirement in unFinishedRequirements)
            {
                checkmarkProgress.Add(
                    new RequirementProgress
                    {
                        Id = unFinishedRequirement.Id,
                        RequirementName = unFinishedRequirement.Requirement!.Name,
                        IsCompleted = false
                    }
                );
            }

            DrivingLessonProgress? drivingRequirementProgress = null;
            if (drivingRequirement != null)
            {
                drivingRequirementProgress = new DrivingLessonProgress
                {
                    Completed = await _bll.ContractCourses.GetDrivingLessonHours(studentCourseId),
                    Needed = drivingRequirement.Amount!.Value
                };
            }

            return Ok(new StudentCourseReport
            {
                Id = studentCourse.Id,
                CourseName = studentCourse.Course!.Name,
                DrivingRequirementProgress = drivingRequirementProgress,
                CheckmarkProgress = checkmarkProgress
            });
        }


        /// <summary>
        /// Update Student course requirements
        /// </summary>
        /// <param name="dto">Student Contract Report used to get Checkmark progress</param>
        /// <returns>Student Course Report result</returns>
        [HttpPut]
        [ActionName("CourseReport")]
        [ProducesResponseType(typeof(StudentCourseReport), 200)]
        [ProducesResponseType(typeof(Message), 401)]
        [ProducesResponseType(typeof(Message), 404)]
        [ProducesResponseType(typeof(Message), 500)]
        public async Task<ActionResult<StudentCourseReport>> UpdateStudentCourseRequirements(StudentCourseReport dto)
        {
            var studentContractCourse = await _bll.ContractCourses.FirstOrDefaultAsync(dto.Id);
            if (studentContractCourse == null)
            {
                return NotFound(new Message($"Student not found"));
            }

            var authenticationCheck = await _bll.DrivingSchools.HasUserWithTitle(User.GetUserId()!.Value,
                studentContractCourse.Course!.DrivingSchoolId!.Value, Domain.App.Constants.Titles.Teacher);
            if (!authenticationCheck)
            {
                return Unauthorized(new Message(Resource.UnAuthorized));
            }

            foreach (var checkmark in dto.CheckmarkProgress)
            {
                await _bll.Lessons.DeleteContractCourseRequirementLessons(dto.Id, checkmark.Id);
                var currentTime = DateTime.Now;
                if (checkmark.IsCompleted)
                {
                    var lesson = new Lesson
                    {
                        Start = currentTime,
                        End = currentTime,
                        CourseRequirementId = checkmark.Id
                    };
                    lesson = _bll.Lessons.Add(lesson);
                    await _bll.SaveChangesAsync();
                    
                    var courseRequirement = await _bll.CourseRequirements.FirstOrDefaultAsync(checkmark.Id);


                    _bll.LessonParticipants.Add(new LessonParticipant
                    {
                        LessonId = lesson.Id,
                        ContractCourseId = dto.Id,
                        Price = courseRequirement!.Price,
                        OccurrenceConfirmation = true,
                        Start = currentTime,
                        End = currentTime,
                        LessonNote = ""
                    });
                }
            }
            
            await _bll.SaveChangesAsync();
            return Ok(dto);
        }

        /// <summary>
        ///     Give student a course
        /// </summary>
        /// <param name="dto">Student Course DTO used for making a new Contract Course for the student</param>
        /// <returns>Created Student Course</returns>
        [HttpPost]
        [ActionName("Course")]
        [ProducesResponseType(typeof(StudentCourse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StudentCourse>> AddCourse(StudentCourse dto)
        {
            if (!await _bll.Courses.ExistsAsync(dto.CourseId))
            {
                return BadRequest(new Message($"Course with id of {dto.CourseId} does not exist!"));
            }

            var activeStatus = await _bll.Statuses.GetStatusByName(Domain.App.Constants.Statuses.Active);
            var contractCourse = new BLL.App.DTO.ContractCourse
            {
                HourlyPay = 0,
                StatusId = activeStatus!.Id,
                ContractId = dto.ContractId,
                CourseId = dto.CourseId,
                LessonParticipants = new List<LessonParticipant>()
            };

            contractCourse = _bll.ContractCourses.Add(contractCourse);
            await _bll.SaveChangesAsync();
            dto.Id = contractCourse.Id;
            return Ok(dto);
        }
        
        /// <summary>
        ///     Delete student course
        /// </summary>
        /// <param name="id">ID of contract course that belongs to a student</param>
        /// <returns>Removed Student Course</returns>
        [ActionName("CourseDelete")]
        [ProducesResponseType(typeof(PublicAPI.DTO.v1.Entities.StudentCourse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<StudentCourse>> DeleteCourse(Guid id)
        {
            
            var contractCourse = await _bll.ContractCourses.FirstOrDefaultAsync(id);
            if (contractCourse == null) return NotFound(new Message($"There is no Contract with id {id.ToString()}"));
            var contract = await _bll.Contracts.FirstOrDefaultAsync(contractCourse.ContractId!.Value);
            var isAuthorized = await _bll.DrivingSchools.HasUserWithTitle(User.GetUserId()!.Value,
                contract!.DrivingSchoolId!.Value, Domain.App.Constants.Titles.Teacher);
            if (contract!.AppUserId != User.GetUserId()!.Value | !isAuthorized) return Unauthorized(new Message(Resource.UnAuthorized));
            contractCourse = await _bll.ContractCourses.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return Ok(_studentCourseMapper.Map(contractCourse));
        }
    }
}