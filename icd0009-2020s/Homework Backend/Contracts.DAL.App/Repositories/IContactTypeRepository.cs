using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IContactTypeRepository: IBaseRepository<ContactType>, IContactTypeRepositoryCustom<ContactType>
    {
    }

    public interface IContactTypeRepositoryCustom<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllWithContactCountAsync(bool noTracking = true);
        Task<int> GetPersonUniqueContactTypeCounts(Guid personId);
    }
 
}