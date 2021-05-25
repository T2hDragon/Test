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
using Lesson = DAL.App.DTO.Lesson;
using Requirement = DAL.App.DTO.Requirement;

namespace DAL.App.EF.Repositories
{
    public class LessonRepository : BaseRepository<DAL.App.DTO.Lesson, Domain.App.Lesson, AppDbContext>, ILessonRepository
    {
        public LessonRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new LessonMapper(mapper))
        {
        }

        public override async Task<Lesson?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);

            var queryResult = await query
                .Include(lesson => lesson.CourseRequirement)
                .ThenInclude(courseRequirement => courseRequirement.Requirement)
                .ThenInclude(requirement => requirement!.Name)
                .ThenInclude(s => s.Translations)
                .AsSingleQuery()
                .FirstOrDefaultAsync(x => x.Id == id);
            
            return Mapper.Map(queryResult);

        }

        public async Task DeleteContractCourseRequirementLessons(Guid contractCourseId, Guid courseRequirementId)
        {
            var query = CreateQuery();

            var queryResult = await query
                .Include(lesson => lesson.LessonParticipants)
                .Where(lesson => lesson.CourseRequirementId == courseRequirementId & lesson.LessonParticipants!.Any(participant => participant.ContractCourseId == contractCourseId))
                .ToListAsync();
            RepoDbSet.RemoveRange(queryResult);
        }
    }
}