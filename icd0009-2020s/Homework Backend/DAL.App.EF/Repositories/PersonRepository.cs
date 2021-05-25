using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base.Repositories;
using DAL.App.EF.Mapper;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PersonRepository : BaseRepository<DAL.App.DTO.Person, Domain.App.Person, AppDbContext>, IPersonRepository
    {
        public PersonRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new PersonMapper(mapper))
        {
        }
        
        public override async Task<IEnumerable<DAL.App.DTO.Person>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            query = query
                .Include(p => p.Contacts)
                .ThenInclude(c => c.ContactType);
            if (userId != default)
            {
                query = query
                    .Where(c => c.AppUserId == userId);
            }

            var res = await query.Select(x => Mapper.Map(x)).ToListAsync();


            return res!;
        }

        public override async Task<DAL.App.DTO.Person?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            query = query
                .Include(p => p.Contacts)
                .ThenInclude(c => c.ContactType);

            var res = await query.FirstOrDefaultAsync(m => m.Id == id && m.AppUserId == userId);

            return Mapper.Map(res);
        }

        public async Task<IEnumerable<DAL.App.DTO.Person>> GetAllWithContactCountsAsync(Guid userId, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            var resQuery = query.Select(p => new DAL.App.DTO.Person()
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    ContactCount = p.Contacts!.Count
                })
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName);

            return await resQuery.ToListAsync();
        }
    }
}