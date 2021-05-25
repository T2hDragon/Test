using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IContractRepository: IBaseRepository<Contract>, IContractRepositoryCustom<Contract>
    {
        Task<Contract?> FirstOrDefaultWithCoursesAsync(Guid id, Guid userId = default, bool noTracking = true);
    }
    // Add User custom method declarations
    public interface IContractRepositoryCustom<TEntity>
    {
        Task<IEnumerable<TEntity?>> GetContractsBySchool(Guid schoolId, string? title = null, string? status = null, bool noTracing = true);
        public Task<string> GetContractorName(Guid contractId, bool noTracing = true);

        public Task<TEntity?> GetContractByUsername(string username, Guid schoolId, string title, string status);
        public Task<IEnumerable<TEntity>> GetSchoolContractsByUsername(string username, Guid schoolId, string? title = null, string? status = null);
        public Task<IEnumerable<TEntity>> GetSchoolContractsByName(string name, Guid schoolId, string? title = null, string? status = null);
        public Task<IEnumerable<TEntity>> GetSchoolContracts(Guid schoolId, string username = "", string fullname = "", string? title = null, string? status = null);
        public Task<IEnumerable<TEntity>> GetContractsByAppUser(Guid appUserId);
        public Task<IEnumerable<TEntity>> GetLessonContractsByTitle(Guid lessonId, string title);

        public Task<bool> IsFree(Guid contractId, DateTime startingDate, DateTime endingDate);
        public Task<TEntity?> GetSchoolContractWithTitle(Guid appUserId, Guid schoolId, string title, string status);

    }
}