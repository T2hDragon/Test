using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.BLL.App;
using Extensions.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicAPI.DTO.v1;
using ContractDTO = PublicAPI.DTO.v1.Entities.Contract;
using PublicAPI.DTO.v1.Mappers;
using Resource = Resources.Errors;

namespace WebApp.ApiControllers.Entities
{
    /// <summary>
    /// Controller for handling Contract Domain entity
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ContractsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private ContractMapper _contractMapper;
        private CourseMapper _courseMapper;


        /// <summary>
        /// Mapper and db layer construction
        /// </summary>
        /// <param name="bll">Business Layer Logic</param>
        /// <param name="mapper">Mapper</param>
        public ContractsController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _contractMapper = new ContractMapper(mapper);
            _courseMapper = new CourseMapper(mapper);
        }
        
        /// <summary>
        /// Get logged in user contracts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetAll")]
        [ProducesResponseType(typeof(IEnumerable<ContractDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<ContractDTO>>> GetContracts()
        {
            if (User.GetUserId() == null | User.GetUserId() == Guid.Empty)
            {
                return Unauthorized(new Message(Resource.NotLoggedIn));
            }
            
            var contracts =
                await _bll.Contracts.GetContractsByAppUser(User.GetUserId()!.Value);
            var apiContracts = contracts.Select(x => _contractMapper.Map(x));
            return Ok(apiContracts);
        }


        /// <summary>
        /// Get contract with the given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ActionName("Get")]
        [ProducesResponseType(typeof(ContractDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ContractDTO>> GetContract(Guid id)
        {
            var contract = await _bll.Contracts.FirstOrDefaultAsync(id);
            if (contract == null) return NotFound(new Message(String.Format(Resource.NoTypeWithOf, "Contract", "id", id.ToString())));
            
            var isAuthorized = await _bll.DrivingSchools.HasUserWithTitle(User.GetUserId()!.Value,
                contract!.DrivingSchoolId!.Value, Domain.App.Constants.Titles.Teacher);
            
            if (contract.AppUserId != User.GetUserId()!.Value & !isAuthorized) return Unauthorized(new Message(Resource.UnAuthorized));

            return Ok(_contractMapper.Map(contract));
        }
        
        /// <summary>
        /// Get Contract missing courses
        /// </summary>
        /// <param name="contractId">Id of the contract in hand</param>
        /// <returns></returns>
        [HttpGet("{contractId}")]
        [ActionName("MissingCourses")]
        [ProducesResponseType(typeof(PublicAPI.DTO.v1.Entities.Course), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicAPI.DTO.v1.Entities.Course>> GetMissingCourses(Guid contractId)
        {
            var contract = await _bll.Contracts.FirstOrDefaultAsync(contractId);
            if (contract == null) return NotFound(new Message(String.Format(Resource.NoTypeWithOf, "Contract", "id", contractId.ToString())));
            var courses = (await _bll.Courses.GetContractMissingCourses(contractId)).ToList();
            if (await _bll.DrivingSchools.HasUserWithTitle(User.GetUserId()!.Value, contract.DrivingSchoolId!.Value,
                Domain.App.Constants.Titles.Teacher))
            {
                var teacherContract = await _bll.Contracts.GetSchoolContractWithTitle(User.GetUserId()!.Value,
                    contract.DrivingSchoolId!.Value,
                    Domain.App.Constants.Titles.Teacher, Domain.App.Constants.Statuses.Active);
                var notPermittedCourses = (await _bll.Courses.GetContractMissingCourses(teacherContract!.Id)).ToList();
                var permittedCheckedCourses = new List<BLL.App.DTO.Course>();
                foreach (var course in courses)
                {
                    if (notPermittedCourses!.Any(notPermittedCourse => notPermittedCourse!.Id == course.Id)) continue;
                    permittedCheckedCourses.Add(course);
                }
                return Ok(permittedCheckedCourses.Select(x => _courseMapper.Map(x)));
            }

            return Ok(courses.Select(x => _courseMapper.Map(x)));
        }


        /// <summary>
        /// Accept an invitation
        /// </summary>
        /// <param name="id">Id of the contract with the invitation</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ActionName("AcceptInvite")]
        [ProducesResponseType(typeof(ContractDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AcceptInvite(Guid id)
        {
            var contract = await _bll.Contracts.FirstOrDefaultAsync(id);
            if (contract == null) return NotFound(new Message(String.Format(Resource.NoTypeWithOf, "Contract", "id", id.ToString())));
            if (contract.AppUserId != User.GetUserId()!.Value) return Unauthorized(new Message(Resource.UnAuthorized));
            var activeStatus = await _bll.Statuses.GetStatusByName(Domain.App.Constants.Statuses.Active);
            contract.StatusId = activeStatus!.Id;
            contract.Status = activeStatus;
            _bll.Contracts.Update(contract);
            await _bll.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// Decline an invitation
        /// </summary>
        /// <param name="id">Id of the contract with the invitation</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ActionName("DeclineInvite")]
        [ProducesResponseType(typeof(ContractDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeclineInvite(Guid id)
        {
            var contract = await _bll.Contracts.FirstOrDefaultAsync(id);
            if (contract == null) return NotFound(new Message(String.Format(Resource.NoTypeWithOf, "Contract", "id", id.ToString())));
            if (contract.AppUserId != User.GetUserId()!.Value) return Unauthorized(new Message(Resource.UnAuthorized));
            /*var inactiveStatus = await _bll.Statuses.GetStatusByName(Domain.App.Constants.Statuses.Inactive);
            contract.StatusId = inactiveStatus!.Id;
            contract.Status = inactiveStatus;
            _bll.Contracts.Update(contract);*/
            await _bll.Contracts.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return Ok();
        }


        /// <summary>
        /// Remove contract 
        /// </summary>
        /// <param name="id">Contract Id</param>
        /// <returns></returns>
        [ActionName("Delete")]
        [ProducesResponseType(typeof(ContractDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ContractDTO>> DeleteContract(Guid id)
        {
            
            var contract = await _bll.Contracts.FirstOrDefaultAsync(id);
            if (contract == null) return NotFound(new Message(String.Format(Resource.NoTypeWithOf, "Contract", "id", id.ToString())));

            var isAuthorized = await _bll.DrivingSchools.HasUserWithTitle(User.GetUserId()!.Value,
                contract!.DrivingSchoolId!.Value, Domain.App.Constants.Titles.Teacher);

            if (contract.AppUserId != User.GetUserId()!.Value & !isAuthorized) return Unauthorized(new Message(Resource.UnAuthorized));
            await _bll.Contracts.RemoveAsync(contract.Id);
            await _bll.SaveChangesAsync();
            return Ok(_contractMapper.Map(contract));
        }
    }
}