using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mapper;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ContactRepository : BaseRepository<DAL.App.DTO.Contact, Domain.App.Contact, AppDbContext>, IContactRepository
    {
        public ContactRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new ContactMapper(mapper))
        {
        }
        
        public override async Task<IEnumerable<DAL.App.DTO.Contact>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            var resQuery = query
                .Include(p => p.Person)
                .Include(p => p.ContactType)
                .Where(c => c.Person!.AppUserId == userId)
                .Select(x => Mapper.Map(x));
            
            var res = await resQuery.ToListAsync();

            
            return res!;
        }
        
        
        public override async Task<DAL.App.DTO.Contact?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            query = query
                .Include(p => p.Person)
                .Include(c => c.ContactType);
                
            var res = Mapper.Map(await query.FirstOrDefaultAsync(m => m.Id == id && m.Person!.AppUserId == userId));

            return res;
        }
    }
}