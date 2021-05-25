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
using ContractDTO = PublicAPI.DTO.v1.Entities.Contract;
using Student = PublicAPI.DTO.v1.Entities.Student;

namespace WebApp.ApiControllers.Entities
{
    /// <summary>
    ///     Controller with functionality for managing Business Logic Layer Driving Lesson objects.
    /// </summary>
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/[controller]/[action]")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ApiController]
        public class DrivingSchoolsController : ControllerBase
        {
            private readonly IAppBLL _bll;
            private readonly DrivingSchoolMapper _drivingSchoolMapper;

            /// <summary>
            ///     Constructor for Driving Lessons controller
            /// </summary>
            /// <param name="bll">Business logic layer</param>
            /// <param name="mapper">Mapper</param>
            public DrivingSchoolsController(IAppBLL bll, IMapper mapper)
            {
                _bll = bll;
                _drivingSchoolMapper = new DrivingSchoolMapper(mapper);
            }
            
            /// <summary>
            ///     Invite student to a school
            /// </summary>
            /// <param name="studentInvite">Student invitation DTO</param>
            /// <returns>Boolean if it was successful</returns>
            /// <response code="200">Student was successfully invited.</response>
            /// <response code="401">User is either not in correct role or not logged in.</response>
            /// <response code="404">Invitation school was not found.</response>
            [HttpPost]
            [ActionName("InviteStudent")]
            [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
            [ProducesResponseType(typeof(Message), StatusCodes.Status401Unauthorized)]
            [ProducesResponseType(typeof(Message), StatusCodes.Status404NotFound)]
            [ProducesResponseType(typeof(Message), StatusCodes.Status400BadRequest)]
            public async Task<ActionResult<bool>> InviteStudent(StudentInvite studentInvite)
            {
                if (!await _bll.DrivingSchools.HasUserWithTitle(User.GetUserId()!.Value, studentInvite.SchoolId, Domain.App.Constants.Titles.Teacher))
                {
                    return Unauthorized(new Message($"Unauthorized access"));
                }
                if (await _bll.DrivingSchools.HasUserWithTitle(studentInvite.Username, studentInvite.SchoolId, Domain.App.Constants.Titles.Student))
                {
                    return BadRequest(new Message(String.Format(Resources.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber.UserAlreadyInvited, studentInvite.Username)));
                }

                
                var result = await _bll.DrivingSchools.InviteUserToSchool(studentInvite.SchoolId, studentInvite.Username, Domain.App.Constants.Titles.Student);
                if (!result)
                {
                    return NotFound(new Message(String.Format(Resources.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber.UserNotFound, studentInvite.Username)));
                }
                await _bll.SaveChangesAsync();
                return Ok(result);
            }

            /// <summary>
            /// Get all school students using filters.
            /// </summary>
            /// <param name="schoolId">Handling school ID</param>
            /// <param name="fullName">Filter using the users full name</param>
            /// <param name="username">Filter using the users UserName</param>
            /// <returns></returns>
            /// <response code="200">Student was successfully invited.</response>
            /// <response code="401">User is either not in correct role or not logged in.</response>
            [HttpGet("{schoolId}/{fullName}/{username}")]
            [ActionName("Students")]
            [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
            [ProducesResponseType(typeof(Message), StatusCodes.Status401Unauthorized)]
            public async Task<ActionResult<IEnumerable<Student>>> GetStudents(Guid schoolId, string? fullName = "", string? username = "")
            {
                if (fullName == null) fullName = "";
                if (username == null) username = "";
                if (!await _bll.DrivingSchools.HasUserWithTitle(User.GetUserId()!.Value, schoolId, Domain.App.Constants.Titles.Teacher))
                {
                    return Unauthorized(new Message($"Unauthorized access"));
                }
                
                var contracts = await _bll.Contracts.GetSchoolContracts(schoolId, username, fullName , Domain.App.Constants.Titles.Student);
                var students = contracts.Select(contract => new Student
                {
                    Id = contract.Id,
                    Username = contract.AppUser!.UserName,
                    FullName = contract.AppUser.FullName,
                    Email = contract.AppUser.Email
                });
                return Ok(students);
            }
            

            /// <summary>
            /// Get Contract Driving School
            /// </summary>
            /// <param name="contractId">Contract ID</param>
            /// <returns>Driving school</returns>
            /// <response code="404">Contract was not found.</response>
            [HttpGet("{contractId}")]
            [ActionName("DrivingSchoolByContract")]
            [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
            [ProducesResponseType(typeof(Message), StatusCodes.Status404NotFound)]
            public async Task<ActionResult<PublicAPI.DTO.v1.Entities.DrivingSchool>> GetContractSchool(Guid contractId)
            {

                var contract = await _bll.Contracts.FirstOrDefaultAsync(contractId);
                if (contract == null ) return NotFound(new Message(String.Format("Contract with ID of " + contractId.ToString() + " was not found!")));
                var school = await _bll.DrivingSchools.FirstOrDefaultAsync(contract!.DrivingSchoolId!.Value);
                return Ok(_drivingSchoolMapper.Map(school));
            }
            

        }
}