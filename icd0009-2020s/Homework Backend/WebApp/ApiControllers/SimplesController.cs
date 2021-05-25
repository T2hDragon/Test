using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]

    public class SimplesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAppBLL _bll;
        public SimplesController(AppDbContext context, IAppBLL bll)
        {
            _context = context;
            _bll = bll;
        }

        // GET: api/Simples
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<PublicAPI.DTO.v1.Simple>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PublicAPI.DTO.v1.Simple>>> GetSimples()
        {
            return Ok((await _context.Simples.ToListAsync()).Select(s => new PublicAPI.DTO.v1.Simple()
            {
                Payload = s.Payload,
                AppUserId = s.AppUserId
            }));
        }

        /// <summary>
        /// Get one Simple Based on parameter: Id
        /// </summary>
        /// <param name="id"> Id of object to retrieve, Guid</param>
        /// <returns>Simple Entity from database</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicAPI.DTO.v1.Simple), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicAPI.DTO.v1.Simple>> GetSimple(Guid id)
        {
            var simple = await _context.Simples.FindAsync(id);

            if (simple == null)
            {
                return NotFound();
            }

            var result = new PublicAPI.DTO.v1.Simple
            {
                Payload = simple.Payload,
                AppUserId = simple.AppUserId
            };

            return result;
        }

        // PUT: api/Simples/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSimple(Guid id, Simple simple)
        {
            if (id != simple.Id)
            {
                return BadRequest();
            }

            _context.Entry(simple).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SimpleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Simples
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Simple>> PostSimple(Simple simple)
        {
            _context.Simples.Add(simple);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSimple", new { id = simple.Id }, simple);
        }

        // DELETE: api/Simples/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSimple(Guid id)
        {
            var simple = await _context.Simples.FindAsync(id);
            if (simple == null)
            {
                return NotFound();
            }

            _context.Simples.Remove(simple);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        private bool SimpleExists(Guid id)
        {
            return _context.Simples.Any(e => e.Id == id);
        }
        
    }
}
