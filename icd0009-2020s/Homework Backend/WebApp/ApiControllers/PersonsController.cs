using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Extensions.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using PublicAPI.DTO.v1;
using PublicAPI.DTO.v1.Mappers;
using Person = Domain.App.Person;

namespace WebApp.ApiControllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PersonsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAppBLL _bll;
        private readonly IMapper Mapper;

        private PersonMapper _personMapper;
        

        public PersonsController(AppDbContext context, IAppBLL bll, IMapper mapper)
        {
            _context = context;
            _bll = bll;
            Mapper = mapper;
            _personMapper = new PersonMapper(Mapper);
        }

        // GET: api/Persons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BLL.App.DTO.Person>>> GetPersons()
        {
            return Ok(await _bll.Persons.GetAllPersonsWithInfo(User.GetUserId()!.Value));
        }

        // GET: api/Persons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(Guid id)
        {
            var person = await _context.Persons.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        // PUT: api/Persons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(Guid id, Person person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }

            _context.Entry(person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
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

        // POST: api/Persons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PublicAPI.DTO.v1.Person>> PostPerson(PersonAdd person)
        {

            var bllPerson = PersonMapper.MapToBll(person);
            
            /*var bllPerson = new BLL.App.DTO.Person()
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
            };*/

            bllPerson.AppUserId = User.GetUserId()!.Value;
            
            var addedPerson = _bll.Persons.Add(bllPerson);
            
            // bll will call dal.SaveChangesAsync => will call EF.SaveChangesAsync()
            // ef will update entities with new ID-s
            await _bll.SaveChangesAsync();

            var returnPerson = _personMapper.Map(addedPerson);
            /*
             var returnPerson = new PublicAPI.DTO.v1.Person()
            {
                Id = addedPerson.Id,
                FirstName = addedPerson.FirstName,
                LastName = addedPerson.LastName,
                ContactCount = addedPerson.ContactCount ?? -1,
                FullName = addedPerson.FullName
            };*/

            return CreatedAtAction("GetPerson", new {id = returnPerson?.Id}, returnPerson);
        }

        // DELETE: api/Persons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(Guid id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonExists(Guid id)
        {
            return _context.Persons.Any(e => e.Id == id);
        }
    }
}