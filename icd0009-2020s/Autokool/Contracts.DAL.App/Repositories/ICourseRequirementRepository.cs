using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;
namespace Contracts.DAL.App.Repositories
{
    public interface ICourseRequirementRepository : IBaseRepository<CourseRequirement>, ICourseRequirementRepositoryCustom<CourseRequirement>
    {
    }

    // Add User custom method declarations
        public interface ICourseRequirementRepositoryCustom<TEntity>
        {
            public Task<IEnumerable<TEntity>> GetAllByCourseId(Guid courseId, bool noTracing = true);
            public Task<IEnumerable<TEntity>> GetAllCheckmarkableByContractCourse(Guid contractCourseId, bool hasFinished, bool noTracing = true);
            public Task<TEntity> CreateWithRequirementFields(Guid requirementId, Guid courseId, bool noTracing = true);
            public Task<TEntity?> GetDrivingRequirement(Guid courseId, bool noTracking = true);
        }
}