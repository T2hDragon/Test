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

namespace DAL.App.EF.Repositories
{
    public class WorkHourRepository : BaseRepository<DAL.App.DTO.WorkHour, Domain.App.WorkHour, AppDbContext>, IWorkHourRepository
    {
        public WorkHourRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new WorkHourMapper(mapper))
        {
        }
    }
}