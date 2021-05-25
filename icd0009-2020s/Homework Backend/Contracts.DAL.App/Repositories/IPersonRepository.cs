using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPersonRepository : IBaseRepository<Person>, IPersonRepositoryCustom<Person>
    {

        
    }

    public interface IPersonRepositoryCustom<TEntity>
    {       
        // add your Person custom method declarations here
        Task<IEnumerable<TEntity>> GetAllWithContactCountsAsync(Guid userId, bool noTracking = true);
    }
    
}