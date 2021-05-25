using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;


namespace Contracts.DAL.App.Repositories
{
    public interface IStatusRepository: IBaseRepository<Status>, IStatusRepositoryCustom<Status>
    {

    }
    // Add User custom method declarations
    public interface IStatusRepositoryCustom<TEntity>
    {
        public Task<TEntity?> GetStatusByName(string name, bool noTracing = true);
    }
}