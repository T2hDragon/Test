using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;
using Extensions.Base;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class IntPkThingsController : ControllerBase
    {
        private readonly AppDbContext _context;        
        private readonly IAppBLL _bll;


        public IntPkThingsController(AppDbContext context, IAppBLL bll)
        {
            _context = context;
            _bll = bll;
        }

        // GET: api/IntPkThings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IntPkThing>>> GetIntPkThings()
        {
            return await _context.IntPkThings.ToListAsync();
        }

        // GET: api/IntPkThings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IntPkThing>> GetIntPkThing(int id)
        {
            var intPkThing = await _context.IntPkThings.FindAsync(id);

            if (intPkThing == null)
            {
                return NotFound();
            }

            return intPkThing;
        }

        // PUT: api/IntPkThings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIntPkThing(int id, IntPkThing intPkThing)
        {
            if (id != intPkThing.Id)
            {
                return BadRequest();
            }

            _context.Entry(intPkThing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IntPkThingExists(id))
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

        // POST: api/IntPkThings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PublicAPI.DTO.v1.IntPkThing>> PostIntPkThing(PublicAPI.DTO.v1.IntPkThingAdd intPkThing)
        {
            var bllEntity = new BLL.App.DTO.IntPkThing()
            {
               Payload = intPkThing.Payload
            };

            
            
            var addedEntity = _bll.IntPkThings.Add(bllEntity);
            
            // bll will call dal.SaveChangesAsync => will call EF.SaveChangesAsync()
            // ef will update entities with new ID-s
            await _bll.SaveChangesAsync();

            var updatedEntity = _bll.IntPkThings.GetUpdatedEntityAfterSaveChanges(bllEntity);

            var returnEntity = new PublicAPI.DTO.v1.IntPkThing()
            {
                Id = updatedEntity.Id,
                Payload = updatedEntity.Payload
            };


            return CreatedAtAction("GetIntPkThing", new { id = returnEntity.Id }, returnEntity);
        }

        // DELETE: api/IntPkThings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIntPkThing(int id)
        {
            var intPkThing = await _context.IntPkThings.FindAsync(id);
            if (intPkThing == null)
            {
                return NotFound();
            }

            _context.IntPkThings.Remove(intPkThing);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IntPkThingExists(int id)
        {
            return _context.IntPkThings.Any(e => e.Id == id);
        }
    }
}
