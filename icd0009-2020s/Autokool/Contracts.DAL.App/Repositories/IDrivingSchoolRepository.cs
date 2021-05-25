using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IDrivingSchoolRepository: IBaseRepository<DrivingSchool>, IDrivingSchoolRepositoryCustom<DrivingSchool>
    {
    }
    // Add User custom method declarations
    public interface IDrivingSchoolRepositoryCustom<TEntity>
    {
        public Task<bool> InviteUserToSchool(Guid schoolId, string username, string title);
        public Task<bool> IsContractInSchoolWithTitle(Guid contractId, Guid schoolId, string title);
        Task<bool> IsOwner(Guid userId, Guid schoolId);

        public Task<TEntity?> GetAppUserDrivingSchool(Guid appUserId, bool noTracking = true);
        public Task<bool> HasUserWithTitle(string username, Guid schoolId, string title, bool noTracking = true);
        public Task<bool> HasUserWithTitle(Guid appUserId, Guid schoolId, string title, bool noTracking = true);
        public Task<TEntity?> GetDrivingSchoolByContract(Guid contractId, bool noTracking = true);
    }
    
}