using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Extensions.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicAPI.DTO.v1.Helper;
using PublicAPI.DTO.v1.Mappers;
using DrivingLessonDTO = PublicAPI.DTO.v1.Entities.DrivingLesson;
using Message = PublicAPI.DTO.v1.Message;
using Resource = Resources.Errors;

namespace WebApp.ApiControllers.Entities
{

    /// <summary>
    ///     Controller with functionality for managing Business Logic Layer Driving Lessons
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class DrivingLessonsController : ControllerBase
    {
        private readonly Contracts.BLL.App.IAppBLL _bll;
        private DrivingLessonMapper _drivingLessonMapper;
        
        
        /// <summary>
        ///     Constructor for Driving Lesson controller
        /// </summary>
        /// <param name="bll">Business logic layer</param>
        /// <param name="mapper">Mapper</param>
        public DrivingLessonsController(Contracts.BLL.App.IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _drivingLessonMapper = new DrivingLessonMapper(mapper);
        }

        
        /// <summary>
        ///     Get Contract course driving lessons
        /// </summary>
        /// <param name="contractCourseId"> contract course ID</param>
        /// <returns>Driving lessons</returns>
        [HttpGet("{contractCourseId}")]
        [ActionName("ContractCourseDrivingLessons")]
        [ProducesResponseType(typeof(IEnumerable<DrivingLessonDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DrivingLessonDTO>>> GetContractCourseDrivingLessons(Guid contractCourseId)
        {
            var drivingLessons =
                await _bll.Lessons.GetContractCourseDrivingLessons(contractCourseId!, DateTime.MinValue, DateTime.MaxValue);
            return Ok(drivingLessons.Select(x => _drivingLessonMapper.Map(x)));
        }

        /// <summary>
        ///     Get contract course driving lessons between certain dates.
        /// </summary>
        /// <param name="contractCourseId">Contract course ID.</param>
        /// <param name="startingDateStr">String representation of starting date filter.</param>
        /// <param name="endingDateStr">String representation of ending date filter.</param>
        /// <returns>Driving lessons</returns>
        [HttpGet("{contractCourseId}/{startingDateStr}/{endingDateStr}")]
        [ActionName("ContractCourseDrivingLessons")]
        [ProducesResponseType(typeof(IEnumerable<DrivingLessonDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DrivingLessonDTO>>> GetContractCourseDrivingLessons(Guid contractCourseId, string startingDateStr, string endingDateStr)
        {
            var startingDate = DateTime.ParseExact(startingDateStr, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            var endingDate = DateTime.ParseExact(endingDateStr, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            var drivingLessons =
                await _bll.Lessons.GetContractCourseDrivingLessons(contractCourseId!, startingDate, endingDate);
            return Ok(drivingLessons);
        }
        
        /// <summary>
        ///     Get contract driving lessons between certain dates.
        /// </summary>
        /// <param name="contractId">Contract ID.</param>
        /// <param name="startingDateStr">String representation of starting date filter.</param>
        /// <param name="endingDateStr">String representation of starting date filter.</param>
        /// <returns></returns>
        [HttpGet("{contractId}/{startingDateStr}/{endingDateStr}")]
        [ActionName("ContractDrivingLessons")]
        [ProducesResponseType(typeof(IEnumerable<DrivingLessonDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DrivingLessonDTO>>> GetContractDrivingLessons(Guid contractId, string startingDateStr, string endingDateStr)
        {
            var startingDate = DateTime.ParseExact(startingDateStr, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            var endingDate = DateTime.ParseExact(endingDateStr, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            var drivingLessons = (await _bll.Lessons.GetContractDrivingLessons(contractId, startingDate, endingDate)).Select(x => _drivingLessonMapper.Map(x))!;
            return Ok(drivingLessons);
        }
        
        /// <summary>
        ///     Remove Driving lesson
        /// </summary>
        /// <param name="id"> Lesson Id </param>
        /// <returns>Removed Driving Lesson</returns>
        [ActionName("Delete")]
        [ProducesResponseType(typeof(DrivingLessonDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<DrivingLessonDTO>> DeleteDrivingLesson(Guid id)
        {
            var lesson = await _bll.Lessons.FirstOrDefaultAsync(id);
            if (lesson == null) return NotFound(new Message(String.Format(Resource.NoTypeWithOf, "Lesson", "id", id.ToString())));
            _bll.Lessons.Remove(lesson);
            await _bll.SaveChangesAsync();
            var drivingLesson = new DrivingLessonDTO
            {
                CourseName = "",
                End = DateTime.MaxValue,
                Start = DateTime.MinValue,
                Id = Guid.Empty,
                Students = "",
                Teachers = "",
            };
       
            return Ok(_drivingLessonMapper.Map(drivingLesson));
        }
        
        /// <summary>
        ///     Create Driving lesson
        /// </summary>
        /// <param name="drivingLessonDto"> Driving Lesson Creation DTO made for driving lesson creation</param>
        /// <returns>Created Driving Lesson</returns>
        [ActionName("Create")]
        [ProducesResponseType(typeof(DrivingLessonDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<DrivingLessonDTO>> CreateDrivingLesson(DrivingLessonCreation drivingLessonDto)
        {
            if (drivingLessonDto.Length <= 0) return BadRequest(new Message(Resource.LessonLengthError));
            var studentCourse = await _bll.ContractCourses.FirstOrDefaultAsync(drivingLessonDto.StudentCourseId);
            if (studentCourse == null) return NotFound(new Message($"Couldn't find studentCourse with id {drivingLessonDto.StudentCourseId.ToString()}"));
            var studentContract = await _bll.Contracts.FirstOrDefaultAsync(studentCourse.ContractId!.Value);
            var authorized = await _bll.DrivingSchools.HasUserWithTitle(User.GetUserId()!.Value, studentContract!.DrivingSchoolId!.Value, Domain.App.Constants.Titles.Teacher);
            if (!authorized) return Unauthorized(new Message(Resource.UnAuthorized));

            var drivingLessonEnd = drivingLessonDto.Start.AddMinutes(drivingLessonDto.Length * 60.0);
            var isTeacherFree =
                await _bll.Contracts.IsFree(drivingLessonDto.TeacherId, drivingLessonDto.Start, drivingLessonEnd);
            if (!isTeacherFree)
                return Conflict(new Message(String.Format(Resource.TeacherIsNotFreeError,
                                            drivingLessonDto.Start.ToString("HH") + ":" + drivingLessonDto.Start.ToString("mm")
                                            ,
                                            drivingLessonEnd.ToString("HH") + ":" + drivingLessonEnd.ToString("mm"))));
            
            var isStudentFree =
                await _bll.Contracts.IsFree(studentCourse.Id, drivingLessonDto.Start, drivingLessonEnd);
            if (!isStudentFree)
                return Conflict(new Message(String.Format(Resource.StudentIsNotFreeErrror,
                    drivingLessonDto.Start.ToString("HH") + ":" + drivingLessonDto.Start.ToString("mm")
                    ,
                    drivingLessonEnd.ToString("HH") + ":" + drivingLessonEnd.ToString("mm"))));


            var drivingRequirement = await _bll.CourseRequirements.GetDrivingRequirement(studentCourse.CourseId!.Value);
            var lesson = new BLL.App.DTO.Lesson
            {
                Start = drivingLessonDto.Start,
                End = drivingLessonEnd,
                CourseRequirementId = drivingRequirement!.Id
            };
            _bll.Lessons.Add(lesson);
            lesson = _bll.Lessons.GetUpdatedEntityAfterSaveChanges(lesson);
            
            var studentParticipant = new BLL.App.DTO.LessonParticipant
            {
                ContractCourseId = drivingLessonDto.StudentCourseId,
                Start = drivingLessonDto.Start,
                End = drivingLessonEnd,
                Price = drivingRequirement.Price,
                LessonId = lesson.Id
            };

            var teacherCourse = await _bll.ContractCourses.GetByContract(drivingLessonDto.TeacherId, studentCourse.CourseId!.Value);
            var teacherParticipant = new BLL.App.DTO.LessonParticipant
            {
                ContractCourseId = teacherCourse.Id,
                Start = drivingLessonDto.Start,
                End = drivingLessonEnd,
                LessonId = lesson.Id
            };

            _bll.LessonParticipants.Add(studentParticipant);
            _bll.LessonParticipants.Add(teacherParticipant);

            await _bll.SaveChangesAsync();

            // TODO make the new drivingLessonDTO
            var drivingLesson = new DrivingLessonDTO
            {
                CourseName = "",
                End = DateTime.MaxValue,
                Start = DateTime.MinValue,
                Id = Guid.Empty,
                Students = "",
                Teachers = "",
            };
       
            return Ok(_drivingLessonMapper.Map(drivingLesson));
        }

    }
}