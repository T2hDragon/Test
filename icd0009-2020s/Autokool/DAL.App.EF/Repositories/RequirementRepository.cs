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
using Domain.Base;
using Microsoft.EntityFrameworkCore;
using Course = DAL.App.DTO.Course;
using Requirement = DAL.App.DTO.Requirement;

namespace DAL.App.EF.Repositories
{
    public class RequirementRepository : BaseRepository<DAL.App.DTO.Requirement, Domain.App.Requirement, AppDbContext>, IRequirementRepository
    {
        public RequirementRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new RequirementMapper(mapper))
        {
        }

        public override async Task<Requirement?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            var resQuery = query
                .Include(ds => ds.Name)
                .ThenInclude(name => name.Translations)
                .Include(ds => ds.Description)
                .ThenInclude(name => name.Translations)
                .AsSingleQuery()
                .FirstOrDefaultAsync(ds => ds.Id == id);
            
            var res = Mapper.Map(await resQuery);
            
            return res!;        }

        public override Requirement Update(Requirement entity)
        {
            var domainEntity = Mapper.Map(entity);
            domainEntity!.Name = 
                RepoDbContext.LangStrings
                    .Include(t => t.Translations)
                    .First(x => x.Id == domainEntity.NameId);
            domainEntity!.Description = 
                RepoDbContext.LangStrings
                    .Include(t => t.Translations)
                    .First(x => x.Id == domainEntity.DescriptionId);
            domainEntity!.Description.SetTranslation(entity.Description);
            domainEntity!.Name.SetTranslation(entity.Name);
            
            var updatedEntity = RepoDbSet.Update(domainEntity!).Entity;
            var dalEntity = Mapper.Map(updatedEntity);
            return dalEntity!;
            
        }

        public override async Task<IEnumerable<Requirement>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            var resQuery = query
                .Include(x => x.Name)
                .ThenInclude(t => t!.Translations)
                .Include(x => x.Description)
                .ThenInclude(t => t!.Translations)
                .OrderBy(ds => ds.Id)
                .AsSingleQuery()
                .Select(x => Mapper.Map(x));
            
            var res = await resQuery.ToListAsync();

            
            return res!;        }

        public async Task<IEnumerable<Requirement>> GetCourseMissingRequirements(Guid courseId, bool noTracing = true)
        {
            var query = CreateQuery(default, noTracing);

            var queryRes = await query
                .Include(requirement => requirement.CourseRequirements)
                .Include(ds => ds.Name)
                .ThenInclude(name => name.Translations)
                .Include(ds => ds.Description)
                .ThenInclude(name => name.Translations)
                .Where(requirement => requirement.CourseRequirements!.All(courseRequirement => courseRequirement.CourseId != courseId))
                .OrderBy(requirement => requirement.Id)
                .AsSingleQuery()
                .ToListAsync();
            

            return (queryRes.Select(x => Mapper.Map(x)) ?? new List<Requirement>())!;
        }

        public async Task<IEnumerable<Translation>> GetRequirementDescriptionTranslations(Guid requirementId, bool noTracing = true)
        {
            var query = CreateQuery(default, noTracing);

            var requirement = await query
                .Include(ds => ds.Name)
                .ThenInclude(name => name.Translations)
                .Include(ds => ds.Description)
                .ThenInclude(name => name.Translations)
                .OrderBy(requirement => requirement.Id)
                .AsSingleQuery()
                .FirstOrDefaultAsync(requirement => requirement.Id == requirementId);
            var translations = new List<Translation>();
            foreach (var translation in requirement.Description.Translations!)
            {
                translations.Add(translation);
            }

            return translations;
        }
    }
}