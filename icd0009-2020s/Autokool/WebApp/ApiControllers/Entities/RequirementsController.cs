using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicAPI.DTO.v1.Mappers;
using RequirementDTO = PublicAPI.DTO.v1.Entities.Requirement;
using Message = PublicAPI.DTO.v1.Message;
using Resource = Resources.Errors;

namespace WebApp.ApiControllers.Entities
{    
    /// <summary>
    ///     Controller with full CRUD functionality for managing Requirement objects.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Roles = Domain.App.Constants.AppRoles.Administrator)]
    [ApiController]
    public class RequirementsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private RequirementMapper _requirementMapper;

        /// <summary>
        ///     Constructor for Requirement controller
        /// </summary>
        /// <param name="bll">Business logic layer</param>
        /// <param name="mapper">Mapper</param>
        public RequirementsController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _requirementMapper = new RequirementMapper(mapper);
        }

        /// <summary>
        ///     Get all Requirement objects.
        /// </summary>
        /// <returns>Array of Requirement objects</returns>
        /// <response code="200">Requirements were successfully retrieved from data source.</response>
        /// <response code="401">User is either not in correct role or not logged in.</response>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<RequirementDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<RequirementDTO>>> GetRequirements()
        {
            var requirements =
                await _bll.Requirements.GetAllAsync();
            return Ok(requirements.Select(x => _requirementMapper.Map(x)));
        }

        /// <summary>
        ///     Get Requirement with specified ID.
        /// </summary>
        /// <param name="id">Requirement ID - GUID</param>
        /// <returns>Requirement with specified ID.</returns>
        /// <response code="200">Requirement was successfully found and retrieved from data source.</response>
        /// <response code="401">User is either not in correct role or not logged in.</response>
        /// <response code="404">Requirement with specified ID.</response>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(RequirementDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RequirementDTO>> GetRequirement(Guid id)
        {
            var requirement = await _bll.Requirements.FirstOrDefaultAsync(id);

            if (requirement == null) return NotFound(new Message($"There is no requirement with id {id.ToString()}"));
            return Ok(_requirementMapper.Map(requirement));
        }

        /// <summary>
        ///     Update Requirement with specified ID stored inside data source.
        /// </summary>
        /// <param name="id">Requirement ID - GUID</param>
        /// <param name="dto">Requirement dto</param>
        /// <returns>
        ///     Updated with Requirement specified ID.
        /// </returns>
        /// <response code="200">
        ///     Requirement was successfully updated inside data source.
        /// </response>
        /// <response code="401"> User is either not in correct role or not logged in.</response>
        /// <response code="404">Requirement with specified ID belonging to logged-in user was not found</response>
        /// <response code="400">ID parameter does not match the ID of received with Requirement</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(RequirementDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutRequirement(Guid id, RequirementDTO dto)
        {
            if (id != dto.Id) return BadRequest();
            var requirement = await _bll.Requirements.FirstOrDefaultAsync(id);
            if (requirement == null) return NotFound(new Message($"There is no requirement with id {id.ToString()}"));
            var updatedRequirement = _requirementMapper.Map(dto, requirement)!;
            _bll.Requirements.Update(updatedRequirement);
            await _bll.SaveChangesAsync();
            return Ok(_requirementMapper.Map(requirement));
        }

        /// <summary>
        ///     Add new Requirement object.
        /// </summary>
        /// <param name="dto">Requirement dto</param>
        /// <returns>Requirement object created for logged-in user.</returns>
        /// <response code="200">Requirement object was successfully created.</response>
        /// <response code="401">User is either not in correct role or not logged in.</response>
        [ProducesResponseType(typeof(RequirementDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public async Task<ActionResult<RequirementDTO>> PostRequirement(RequirementDTO dto)
        {
            var requirement = _requirementMapper.Map(dto);
            var result = _bll.Requirements.Add(requirement!);
            await _bll.SaveChangesAsync();
            return Ok(_requirementMapper.Map(result));
        }


        /// <summary>
        ///     Remove Requirement with specified ID.
        /// </summary>
        /// <param name="id">Requirement object ID - GUID</param>
        /// <returns>Requirement object that was deleted.</returns>
        /// <response code="200">Requirement object was successfully deleted from data source.</response>
        /// <response code="404">
        ///     Requirement object with specified ID belonging to logged-in user was not found.
        /// </response>
        /// <response code="401">
        ///     User is not in correct role, not logged in or Title with specified ID does not belong to them.
        /// </response>
        [ProducesResponseType(typeof(RequirementDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<RequirementDTO>> DeleteRequirement(Guid id)
        {
            var requirement = await _bll.Requirements.FirstOrDefaultAsync(id);
            if (requirement == null) return NotFound(new Message($"Couldn't find requirement with id {id.ToString()}"));
            try
            {
                _bll.Requirements.Remove(requirement);
                await _bll.SaveChangesAsync();
            }
            catch (Exception)
            {
                return Conflict(new Message(Resource.HasDependencies));
            }

            return Ok(_requirementMapper.Map(requirement));
        }
    }
}