using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ILessonParticipantRepository: IBaseRepository<LessonParticipant>, ILessonParticipantRepositoryCustom<LessonParticipant>
    {

    }
    // Add User custom method declarations
    public interface ILessonParticipantRepositoryCustom<TEntity>
    {
        Task<IEnumerable<TEntity>> GetContractLessonPartitions(Guid contractId, DateTime? searchStart,
            DateTime? searchEnd, bool noTracking = true);

        Task<IEnumerable<TEntity>> GetContractDrivingLessonPartitions(Guid contractId,
            DateTime? searchStart = null, DateTime? searchEnd = null, bool noTracking = true);
        Task<IEnumerable<TEntity>> GetContractCourseDrivingLessonPartitions(Guid contractCourseId,
            DateTime? searchStart = null, DateTime? searchEnd = null, bool noTracking = true);


    }
}