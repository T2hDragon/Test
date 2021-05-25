using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mapper;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ContractCourseRepository : BaseRepository<DAL.App.DTO.ContractCourse, Domain.App.ContractCourse, AppDbContext>, IContractCourseRepository
    {
        public ContractCourseRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new ContractCourseMapper(mapper))
        {
        }


        public override async Task<ContractCourse?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);

            var queryResult = await query
                .Include(contractCourse => contractCourse.Course)
                .ThenInclude(course => course!.Name)
                .ThenInclude(s => s.Translations)
                .Include(contractCourse => contractCourse.Course)
                .ThenInclude(course => course!.Description)
                .ThenInclude(s => s.Translations)
                .AsSingleQuery()
                .FirstOrDefaultAsync(contractCourse => contractCourse.Id == id);
            return Mapper.Map(queryResult);
        }

        public async Task<IEnumerable<ContractCourse>> GetByContract(Guid contractId, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);

            var queryResult = await query
                .Include(contractCourse => contractCourse.Course)
                .ThenInclude(course => course!.Name)
                .ThenInclude(s => s.Translations)
                .Include(contractCourse => contractCourse.Course)
                .ThenInclude(course => course!.Description)
                .ThenInclude(s => s.Translations)
                .AsSingleQuery()
                .Where(contractCourse => contractCourse.ContractId == contractId)
                .ToListAsync();

            return queryResult.Select(x => Mapper.Map(x))!;


        }

        public async Task<ContractCourse> GetByContract(Guid contractId, Guid courseId, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);

            var queryResult = await query
                .Include(contractCourse => contractCourse.Course)
                .ThenInclude(course => course!.Name)
                .ThenInclude(s => s.Translations)
                .Include(contractCourse => contractCourse.Course)
                .ThenInclude(course => course!.Description)
                .ThenInclude(s => s.Translations)
                .AsSingleQuery()
                .FirstOrDefaultAsync(contractCourse =>
                    contractCourse.ContractId == contractId && contractCourse.CourseId == courseId);
            return Mapper.Map(queryResult)!;        
        }

        public async Task<double> GetDrivingLessonHours(Guid contractCourseId, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);

            var queryResult = await RepoDbContext.LessonParticipants
                .Include(participant => participant.Lesson)
                .ThenInclude(lesson => lesson!.CourseRequirement)
                .ThenInclude(courseRequirement => courseRequirement.Requirement)
                .Where(participant => participant.ContractCourseId == contractCourseId
                                      & participant.Lesson!.CourseRequirement.Requirement!.Amount != null
                                      & participant.End <= DateTime.Now)
                .OrderBy(participant => participant.Start)
                .ToListAsync();
                

            var hours = 0.0;
            foreach (var lessonParticipant in queryResult)
            {
                hours += (lessonParticipant.End - lessonParticipant.Start)!.Value.TotalHours;

            }
            
            return hours;
        }
    }
}