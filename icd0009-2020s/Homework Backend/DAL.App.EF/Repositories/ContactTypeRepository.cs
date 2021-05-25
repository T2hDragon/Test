using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mapper;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;
using ContactType = DAL.App.DTO.ContactType;

namespace DAL.App.EF.Repositories
{
    public class ContactTypeRepository : BaseRepository<DAL.App.DTO.ContactType, Domain.App.ContactType, AppDbContext>, IContactTypeRepository
    {
        public ContactTypeRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new ContactTypeMapper(mapper))
        {
        }


        public override async Task<IEnumerable<DAL.App.DTO.ContactType>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            var resQuery = query
                .Include(p => p.Contacts)
                .Select(x => Mapper.Map(x));

            var res = await resQuery.ToListAsync();


            return res!;
        }

        public override async Task<DAL.App.DTO.ContactType?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking).Include(e => e.Contacts);
            var res = await query.FirstOrDefaultAsync(e => e!.Id.Equals(id));
            return Mapper.Map(res);
        }

        public async Task<IEnumerable<DAL.App.DTO.ContactType>> GetAllWithContactCountAsync(bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);

            var resQuery = query.Select(contactType => new DAL.App.DTO.ContactType()
            {
                Id = contactType.Id,
                Type = contactType.Type,
                ContactCount = contactType.Contacts!.Count
            });

            var res = await resQuery.ToListAsync();
            
            return res;
        }
        
        public async Task<int> GetPersonUniqueContactTypeCounts(Guid personId)
        {
            var query = RepoDbContext
                .Persons
                .Where(p => p.Id == personId)
                .Select(p => p.Contacts!
                    .Select(c => c.ContactTypeId)
                    .Distinct()
                    .Count());

            return await query.FirstAsync();
        }
    }
}