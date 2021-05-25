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
using TitleDTO = PublicAPI.DTO.v1.Entities.Title;
using Message = PublicAPI.DTO.v1.Message;
using Resource = Resources.Errors;

namespace WebApp.ApiControllers.Entities
{
    /// <summary>
    ///     Controller with full CRUD functionality for managing Titles objects.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Domain.App.Constants.AppRoles.Administrator)]
    [ApiController]
    public class TitlesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private TitleMapper _titleMapper;

        /// <summary>
        ///     Constructor for Status controller
        /// </summary>
        /// <param name="bll">Business logic layer</param>
        /// <param name="mapper">Mapper</param>
        public TitlesController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            _titleMapper = new TitleMapper(mapper);
        }
        

        /// <summary>
        ///     Get all Title objects.
        /// </summary>
        /// <returns>Array of Title objects</returns>
        /// <response code="200">Titles were successfully retrieved from data source.</response>
        /// <response code="401">User is either not in correct role or not logged in.</response>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<TitleDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<TitleDTO>>> GetTitles()
        {
            var titles =
                await _bll.Titles.GetAllAsync();
            return Ok(titles.Select(x =>_titleMapper.Map(x)));
        }


        /// <summary>
        ///     Get Title with specified ID.
        /// </summary>
        /// <param name="id">Title ID - GUID</param>
        /// <returns>Title with specified ID.</returns>
        /// <response code="200">Title was successfully found and retrieved from data source.</response>
        /// <response code="401">User is either not in correct role or not logged in.</response>
        /// <response code="404">Title with specified ID.</response>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(TitleDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TitleDTO>> GetTitle(Guid id)
        {
            var title = await _bll.Titles.FirstOrDefaultAsync(id);

            if (title == null) return NotFound(new Message($"There is no Title with id {id.ToString()}"));
            return Ok(_titleMapper.Map(title));
        }


        /// <summary>
        ///     Update Title with specified ID stored inside data source.
        /// </summary>
        /// <param name="id">Title ID - GUID</param>
        /// <param name="dto">Title dto</param>
        /// <returns>
        ///     Updated with Title specified ID.
        /// </returns>
        /// <response code="200">
        ///     Title was successfully updated inside data source.
        /// </response>
        /// <response code="401"> User is either not in correct role or not logged in.</response>
        /// <response code="404">Title with specified ID belonging to logged-in user was not found</response>
        /// <response code="400">ID parameter does not match the ID of received with Title</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TitleDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutTitle(Guid id, TitleDTO dto)
        {
            if (id != dto.Id) return BadRequest();
            var title = await _bll.Titles.FirstOrDefaultAsync(id);
            if (title == null) return NotFound();
            _bll.Titles.Update(_titleMapper.Map(dto, title)!);
            await _bll.SaveChangesAsync();
            return Ok(_titleMapper.Map(title));
        }

        /// <summary>
        ///     Add new Title object.
        /// </summary>
        /// <param name="dto">Title dto</param>
        /// <returns>Title object created for logged-in user.</returns>
        /// <response code="200">Title object was successfully created.</response>
        /// <response code="401">User is either not in correct role or not logged in.</response>
        [ProducesResponseType(typeof(TitleDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public async Task<ActionResult<TitleDTO>> PostTitle(TitleDTO dto)
        {
            var title = _titleMapper.Map(dto);
            var result = _bll.Titles.Add(title!);
            await _bll.SaveChangesAsync();
            return Ok(_titleMapper.Map(result));
        }

        /// <summary>
        ///     Remove Title with specified ID.
        /// </summary>
        /// <param name="id">Title object ID - GUID</param>
        /// <returns>Title object that was deleted.</returns>
        /// <response code="200">Title object was successfully deleted from data source.</response>
        /// <response code="404">
        ///     Title object with specified ID belonging to logged-in user was not found.
        /// </response>
        /// <response code="401">
        ///     User is not in correct role, not logged in or Title with specified ID does not belong to them.
        /// </response>
        [ProducesResponseType(typeof(TitleDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<TitleDTO>> DeleteTitle(Guid id)
        {
            var title = await _bll.Titles.FirstOrDefaultAsync(id);
            if (title == null) return NotFound(new Message($"Couldn't find Title with id {id.ToString()}"));
            if (title == null) return NotFound(new Message($"Couldn't find Title with id {id.ToString()}"));
            try
            {
                _bll.Titles.Remove(title);
                await _bll.SaveChangesAsync();
            }
            catch (Exception)
            {
                return Conflict(new Message(Resource.HasDependencies));
            }

            return Ok(_titleMapper.Map(title));
        }
    }
}