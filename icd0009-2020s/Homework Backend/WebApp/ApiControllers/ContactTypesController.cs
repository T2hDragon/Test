using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers {
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ContactTypesController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;


        public ContactTypesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/ContactTypes
        [HttpGet]
        [ProducesResponseType(typeof(PublicAPI.DTO.v1.JwtResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(PublicAPI.DTO.v1.Message), StatusCodes.Status404NotFound)]
        // [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<DAL.App.DTO.ContactType>>> GetContactTypes()
        {
            /*
            var data = await _uow.ContactTypes.GetAllAsync();
            var res = data.Select(contactType => new ContactTypeDTO()
            {
                ContactTypeValue = contactType.ContactTypeValue,
                ContactCount = contactType.Contacts!.Count
            });
            */
            var res = await _uow.ContactTypes.GetAllWithContactCountAsync();
            return Ok(res);
        }

        // GET: api/ContactTypes/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PublicAPI.DTO.v1.JwtResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(PublicAPI.DTO.v1.Message), StatusCodes.Status404NotFound)]
        // [AllowAnonymous]
        public async Task<ActionResult<DAL.App.DTO.ContactType>> GetContactType(Guid id)
        {
            var contactType = await _uow.ContactTypes.FirstOrDefaultAsync(id);

            if (contactType == null)
            {
                return NotFound();
            }
            Console.WriteLine(contactType.Id.ToString());

            return contactType;
        }

        // PUT: api/ContactTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PublicAPI.DTO.v1.JwtResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(PublicAPI.DTO.v1.Message), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutContactType(Guid id, DAL.App.DTO.ContactType contactType)
        {
            if (id != contactType.Id)
            {
                return BadRequest();
            }

            _uow.ContactTypes.Update(contactType);
                await _uow.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/ContactTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DAL.App.DTO.ContactType>> PostContactType(DAL.App.DTO.ContactType contactType)
        {
            _uow.ContactTypes.Add(contactType);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetContactType", new { id = contactType.Id }, contactType);
        }

        // DELETE: api/ContactTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactType(Guid id)
        {
            
            
            var contactType = await _uow.ContactTypes.FirstOrDefaultAsync(id);
            if (contactType == null)
            {
                return NotFound();
            }

            _uow.ContactTypes.Remove(contactType);
            await _uow.SaveChangesAsync();

            return NoContent();
        }
        
    }
}
