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
using CourseRequirement = DAL.App.DTO.CourseRequirement;

namespace DAL.App.EF.Repositories
{
    public class CourseRequirementRepository : BaseRepository<DAL.App.DTO.CourseRequirement, Domain.App.CourseRequirement, AppDbContext>, ICourseRequirementRepository
    {
        public CourseRequirementRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new CourseRequirementMapper(mapper))
        {
        }

        public override CourseRequirement Update(CourseRequirement entity)
        {
            var domainEntity = Mapper.Map(entity);
            domainEntity!.Description = 
                RepoDbContext.LangStrings
                    .Include(t => t.Translations)
                    .First(x => x.Id == domainEntity.DescriptionId);
            domainEntity!.Description.SetTranslation(entity.Description);

            
            var updatedEntity = RepoDbSet.Update(domainEntity!).Entity;
            var dalEntity = Mapper.Map(updatedEntity);
            return dalEntity!;          }

        public override async Task<IEnumerable<CourseRequirement>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            var resQuery = query
                .Include(x => x.Description)
                .ThenInclude(t => t!.Translations)
                .OrderBy(ds => ds.Id)
                .AsSingleQuery()
                .Include(cr => cr.Requirement)
                .ThenInclude(x => x!.Name)
                .ThenInclude(t => t!.Translations)
                .OrderBy(ds => ds.Id)
                .AsSingleQuery()
                .Include(cr => cr.Course)
                .ThenInclude(x => x!.Name)
                .ThenInclude(t => t!.Translations)
                .OrderBy(ds => ds.Id)
                .AsSingleQuery()
                .Select(x => Mapper.Map(x));
            
            var res = await resQuery.ToListAsync();

            
            return res!;        }
        
        
        
        public async Task<IEnumerable<CourseRequirement>> GetAllByCourseId(Guid courseId, bool noTracing = true)
        {
            var query = CreateQuery(default, noTracing);

            var queryResult = query
                .Include(requirement => requirement.Requirement)
                .ThenInclude(ds => ds!.Name)
                .ThenInclude(name => name.Translations)
                .Include(requirement => requirement.Requirement)
                .ThenInclude(ds => ds!.Description)
                .ThenInclude(name => name.Translations)
                .Include(requirement => requirement.Course)
                .ThenInclude(course => course!.DrivingSchool)
                .Include(ds => ds.Description)
                .ThenInclude(name => name.Translations)
                .Where(requirement => requirement.CourseId == courseId)
                .OrderBy(requirement => requirement.Id)
                .AsSingleQuery()
                .Select(x => Mapper.Map(x));

            return (await queryResult.ToListAsync())!;
        }

        public async Task<IEnumerable<CourseRequirement>> GetAllCheckmarkableByContractCourse(Guid contractCourseId, bool hasFinished, bool noTracing = true)
        {
            var query = CreateQuery(default, noTracing);

            var resQuery = await query
                .Include(courseRequirement => courseRequirement.Requirement)
                .ThenInclude(requirement => requirement!.Name)
                .ThenInclude(s => s.Translations)
                .Include(courseRequirement => courseRequirement.Lessons)
                .ThenInclude(lesson => lesson.LessonParticipants)
                .Include(courseRequirement => courseRequirement.Course)
                .ThenInclude(course => course!.ContractCourses)
                .AsSingleQuery()
                .Where(courseRequirement => 
                    courseRequirement!.Amount == null
                            & courseRequirement.Course!.ContractCourses!.Any(contractCourse => contractCourse.Id == contractCourseId)
                    & courseRequirement.Lessons!.Any(lesson => 
                        lesson.LessonParticipants!.Any(participant => 
                            participant.ContractCourseId == contractCourseId
                            )
                        ) == hasFinished
                )
                .ToListAsync();
            return resQuery.Select(x => Mapper.Map(x))!;
        }

        public async Task<CourseRequirement> CreateWithRequirementFields(Guid requirementId, Guid courseId, bool noTracing = true)
        {
            var query = CreateQuery(default, noTracing);

            var requirement = await RepoDbContext.Requirements
                .Include(ds => ds.Name)
                .ThenInclude(name => name.Translations)
                .Include(ds => ds.Description)
                .ThenInclude(name => name.Translations)
                .AsSingleQuery()
                .FirstOrDefaultAsync(requirement => requirement.Id == requirementId);
            var descriptionTranslations = new List<Translation>();
            foreach (var translation in requirement.Description.Translations!)
            {
                descriptionTranslations.Add(translation);
            }
            
            var courseRequirementDescription = new LangString
            {
                Translations = new List<Translation>()
            };
            
            foreach (var translation in descriptionTranslations!)
            {
                var translationEmptyCopy = new Translation
                {
                    Culture = translation.Culture,
                    Value = translation.Value,
                    LangString = courseRequirementDescription
                };
                courseRequirementDescription.Translations.Add(translationEmptyCopy);
            }
            await RepoDbContext.SaveChangesAsync();
            
            
            var courseRequirement = new CourseRequirement
            {
                Amount = requirement.Amount,
                CourseId = courseId,
                Description = "Empty",
                Price = requirement.Price,
                RequirementId = requirementId
            };

            var domainEntity = Mapper.Map(courseRequirement)!;
            domainEntity.Description = courseRequirementDescription;
            var result = await RepoDbSet.AddAsync(domainEntity);

            await RepoDbContext.SaveChangesAsync();
            return Mapper.Map(result.Entity)!;
        }

        public async Task<CourseRequirement?> GetDrivingRequirement(Guid courseId, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);

            var queryResult = await query
                .Include(requirement => requirement.Requirement)
                .FirstOrDefaultAsync(requirement => requirement.Requirement!.Amount != null & requirement.CourseId == courseId);

            return Mapper.Map(queryResult);
        }
        

        public override async Task<CourseRequirement?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);

            var queryResult = query
                .Include(requirement => requirement.Course)
                .Include(requirement => requirement.Requirement)
                .Include(ds => ds.Description)
                .ThenInclude(name => name.Translations)
                .Where(requirement => requirement.Id == id)
                .AsSingleQuery()
                .Select(x => Mapper.Map(x));

            return (await queryResult.FirstOrDefaultAsync())!;
        }
    }
}