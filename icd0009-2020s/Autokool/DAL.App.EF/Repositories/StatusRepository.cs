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
using Status = DAL.App.DTO.Status;

namespace DAL.App.EF.Repositories
{
    public class StatusRepository : BaseRepository<DAL.App.DTO.Status, Domain.App.Status, AppDbContext>, IStatusRepository
    {
        public StatusRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new StatusMapper(mapper))
        {
        }

        public async Task<Status?> GetStatusByName(string name, bool noTracing = true)
        {
            var query = CreateQuery(default, noTracing);
            var queryRes = await query.FirstOrDefaultAsync(status => status.Name.Trim() == name.Trim());
            return Mapper.Map(queryRes);
        }

    }
}