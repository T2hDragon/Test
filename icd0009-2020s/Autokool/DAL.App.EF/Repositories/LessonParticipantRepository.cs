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
using LessonParticipant = DAL.App.DTO.LessonParticipant;

namespace DAL.App.EF.Repositories
{
    public class LessonParticipantRepository : BaseRepository<DAL.App.DTO.LessonParticipant, Domain.App.LessonParticipant, AppDbContext>, ILessonParticipantRepository
    {
        public LessonParticipantRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new LessonParticipantMapper(mapper))
        {
        }

        public async Task<IEnumerable<LessonParticipant>> GetContractLessonPartitions(Guid contractId, DateTime? searchStart, DateTime? searchEnd, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);

            var queryResult = await query
                .Include(participant => participant.ContractCourse)
                .ThenInclude(contractCourse => contractCourse!.Course)
                .ThenInclude(course => course!.Name)
                .ThenInclude(n => n.Translations)
                .Include(participant => participant.Lesson)
                .Where(participant =>
                    participant.ContractCourse!.ContractId == contractId &
                    participant.Start >= (searchStart ?? DateTime.MinValue) &
                    participant.End <= (searchEnd ?? DateTime.MaxValue)
                )
                .OrderBy(participant => participant.Start)
                .ToListAsync();

            return queryResult.Select(x => Mapper.Map(x))!;
        }
        
        public async Task<IEnumerable<LessonParticipant>> GetContractDrivingLessonPartitions(Guid contractId, DateTime? searchStart = null, DateTime? searchEnd = null, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);

            var queryResult = await query
                .Include(participant => participant.Lesson)
                .ThenInclude(lesson => lesson!.CourseRequirement)
                .ThenInclude(lesson => lesson!.Requirement)
                .Include(participant => participant.ContractCourse)
                .ThenInclude(contractCourse => contractCourse!.Course)
                .ThenInclude(course => course!.Name)
                .ThenInclude(n => n.Translations)
                .Include(participant => participant.Lesson)
                .Where(participant =>
                    participant.ContractCourse!.ContractId == contractId &
                    participant.Start >= (searchStart ?? DateTime.MinValue) &
                    participant.End <= (searchEnd ?? DateTime.MaxValue) &
                    participant.Lesson!.CourseRequirement.Requirement!.Amount != null
                )
                .OrderBy(participant => participant.Start)
                .ToListAsync();

            return queryResult.Select(x => Mapper.Map(x))!;
        }
        
        public async Task<IEnumerable<LessonParticipant>> GetContractCourseDrivingLessonPartitions(Guid contractCourseId, DateTime? searchStart = null, DateTime? searchEnd = null, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);

            var queryResult = await query
                .Include(participant => participant.Lesson)
                .ThenInclude(lesson => lesson!.CourseRequirement)
                .ThenInclude(lesson => lesson!.Requirement)
                .Include(participant => participant.ContractCourse)
                .ThenInclude(contractCourse => contractCourse!.Course)
                .ThenInclude(course => course!.Name)
                .ThenInclude(n => n.Translations)
                .Include(participant => participant.Lesson)
                .Where(participant =>
                    participant.ContractCourse!.Id == contractCourseId &
                    participant.Start >= (searchStart ?? DateTime.MinValue) &
                    participant.End <= (searchEnd ?? DateTime.MaxValue) &
                    participant.Lesson!.CourseRequirement.Requirement!.Amount != null
                )
                .OrderBy(participant => participant.Start)
                .ToListAsync();

            return queryResult.Select(x => Mapper.Map(x))!;
        }
    }
}