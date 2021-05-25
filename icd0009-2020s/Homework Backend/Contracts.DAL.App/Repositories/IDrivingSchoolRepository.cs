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
    }
}