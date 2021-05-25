using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IContractCourseRepository: IBaseRepository<ContractCourse>, IContractCourseRepositoryCustom<ContractCourse>
    {

    }
    public interface IContractCourseRepositoryCustom<TEntity>
    {
        Task<IEnumerable<TEntity>> GetByContract(Guid contractId, bool noTracking = true);
        Task<TEntity> GetByContract(Guid contractId, Guid courseId, bool noTracking = true);
        Task<double> GetDrivingLessonHours(Guid contractCourseId, bool noTracking = true);

    }
}