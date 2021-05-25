using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;


namespace Contracts.DAL.App.Repositories
{
    public interface ITitleRepository: IBaseRepository<Title>, ITitleRepositoryCustom<Title>
    {

    }
    // Add User custom method declarations
    public interface ITitleRepositoryCustom<TEntity>
    {
    }
}