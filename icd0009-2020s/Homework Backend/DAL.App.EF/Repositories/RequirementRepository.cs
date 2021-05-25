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
    public class RequirementRepository : BaseRepository<DAL.App.DTO.Requirement, Domain.App.Requirement, AppDbContext>, IRequirementRepository
    {
        public RequirementRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new RequirementMapper(mapper))
        {
        }
    }
}