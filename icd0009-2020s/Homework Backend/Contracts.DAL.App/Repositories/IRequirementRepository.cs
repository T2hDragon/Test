using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IRequirementRepository: IBaseRepository<Requirement>, IRequirementRepositoryCustom<Requirement>
    {

    }
    // Add User custom method declarations
    public interface IRequirementRepositoryCustom<TEntity>
    {
    }
}