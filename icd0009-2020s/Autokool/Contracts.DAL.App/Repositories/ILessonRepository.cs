using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ILessonRepository: IBaseRepository<Lesson>, ILessonRepositoryCustom<Lesson>
    {

    }
    // Add User custom method declarations
    public interface ILessonRepositoryCustom<TEntity>
    {
        public Task DeleteContractCourseRequirementLessons(Guid contractCourseId, Guid courseRequirementId);

    }
}