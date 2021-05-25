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
using StatusDTO = PublicAPI.DTO.v1.Entities.Status;
using Message = PublicAPI.DTO.v1.Message;
using Resource = Resources.Errors;

namespace WebApp.ApiControllers.Entities
{
    /// <summary>
    ///     Controller with full CRUD functionality for managing Statuses objects.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Domain.App.Constants.AppRoles.Administrator)]
    [ApiController]
    public class StatusesController : ControllerBase
    {
                

        private readonly IAppBLL _bll;
        private StatusMapper _statusMapper;

        /// <summary>
        ///     Constructor for Statuses controller
        /// </summary>
        /// <param name="bll">Business logic layer</param>
        /// <param name="mapper">Mapper</param>
        public StatusesController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _statusMapper = new StatusMapper(mapper);
        }
        
        /// <summary>
        ///     Get all Statuses objects.
        /// </summary>
        /// <returns>Array of Status objects</returns>
        /// <response code="200">Statuses were successfully retrieved from data source.</response>
        /// <response code="401">User is either not in correct role or not logged in.</response>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<StatusDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<StatusDTO>>> GetStatuss()
        {
            var statuses =
                await _bll.Statuses.GetAllAsync();
            return Ok(statuses.Select(x =>_statusMapper.Map(x)));
        }

        /// <summary>
        ///     Get Status with specified ID.
        /// </summary>
        /// <param name="id">Status ID - GUID</param>
        /// <returns>Status with specified ID.</returns>
        /// <response code="200">Status was successfully found and retrieved from data source.</response>
        /// <response code="401">User is either not in correct role or not logged in.</response>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StatusDTO>> GetStatus(Guid id)
        {
            var status = await _bll.Statuses.FirstOrDefaultAsync(id);

            if (status == null) return NotFound(new Message($"There is no Status with id {id.ToString()}"));
            return Ok(_statusMapper.Map(status));
        }


        /// <summary>
        ///     Update Status with specified ID stored inside data source.
        /// </summary>
        /// <param name="id">Status ID - GUID</param>
        /// <param name="dto">Status dto</param>
        /// <returns>
        ///     Updated with Status specified ID.
        /// </returns>
        /// <response code="200">
        ///     Status was successfully updated inside data source.
        /// </response>
        /// <response code="401"> User is either not in correct role or not logged in.</response>
        /// <response code="404">Status with specified ID belonging to logged-in user was not found</response>
        /// <response code="400">ID parameter does not match the ID of received with Status</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutStatus(Guid id, StatusDTO dto)
        {
            if (id != dto.Id) return BadRequest();
            var status = await _bll.Statuses.FirstOrDefaultAsync(id);
            if (status == null) return NotFound();
            _bll.Statuses.Update(_statusMapper.Map(dto, status)!);
            await _bll.SaveChangesAsync();
            return Ok(_statusMapper.Map(status));
        }

        /// <summary>
        ///     Add new Status object.
        /// </summary>
        /// <param name="dto">Status dto</param>
        /// <returns>Status object created for logged-in user.</returns>
        /// <response code="200">Status object was successfully created.</response>
        /// <response code="401">User is either not in correct role or not logged in.</response>
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public async Task<ActionResult<StatusDTO>> PostStatus(StatusDTO dto)
        {
            var status = _statusMapper.Map(dto);
            var result = _bll.Statuses.Add(status!);
            await _bll.SaveChangesAsync();
            return Ok(_statusMapper.Map(result));
        }

        /// <summary>
        ///     Remove Status with specified ID.
        /// </summary>
        /// <param name="id">Status object ID - GUID</param>
        /// <returns>Status object that was deleted.</returns>
        /// <response code="200">Status object was successfully deleted from data source.</response>
        /// <response code="404">
        ///     Status object with specified ID belonging to logged-in user was not found.
        /// </response>
        /// <response code="401">
        ///     User is not in correct role, not logged in or Title with specified ID does not belong to them.
        /// </response>
        [ProducesResponseType(typeof(StatusDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<StatusDTO>> DeleteStatus(Guid id)
        {
            var status = await _bll.Statuses.FirstOrDefaultAsync(id);
            if (status == null) return NotFound(new Message($"Couldn't find Status with id {id.ToString()}"));
        try{
            _bll.Statuses.Remove(status);
            await _bll.SaveChangesAsync();
        }
        catch (Exception)
        {
            return Conflict(new Message(Resource.HasDependencies));
        }            return Ok(_statusMapper.Map(status));
        }
    }
}