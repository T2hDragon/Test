using Contracts.DAL.Base.Repositories;
using Domain.App;

namespace Contracts.DAL.App.Repositories
{
    public interface IIntPkThingRepository: IBaseRepository<IntPkThing, int>, IIntPkThingRepositoryCustom<IntPkThing>
    {
        
    }

    public interface IIntPkThingRepositoryCustom<TEntity>
    {
        
    }
}