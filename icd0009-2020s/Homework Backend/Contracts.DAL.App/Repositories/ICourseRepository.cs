using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ICourseRepository: IBaseRepository<Course>, ICourseRepositoryCustom<Course>
    {

    }
    // Add User custom method declarations
    public interface ICourseRepositoryCustom<TEntity>
    {
    }
}