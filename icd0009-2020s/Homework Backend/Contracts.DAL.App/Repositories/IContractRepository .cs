using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IContractRepository: IBaseRepository<Contract>, IContractRepositoryCustom<Contract>
    {

    }
    // Add User custom method declarations
    public interface IContractRepositoryCustom<TEntity>
    {
    }
}