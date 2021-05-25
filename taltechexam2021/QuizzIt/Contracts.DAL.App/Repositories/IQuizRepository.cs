using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IQuizRepository: IBaseRepository<Quiz>, IQuizRepositoryCustom<Quiz>
    {

    }
    // Add User custom method declarations
    public interface IQuizRepositoryCustom<TEntity>
    {
        public Task<TEntity> InitializeQuiz(string quizName, Guid creatorId, bool noTracing = true);
        public Task<TEntity> PublishQuiz(Guid quizId, bool noTracing = true);
        public Task<IEnumerable<TEntity>> GetUserCreatedQuizzes(Guid creatorId, bool noTracing = true);
        public Task<IEnumerable<TEntity>> GetQuizzesWithSearchData(Guid searcherId, int page, string search, bool noTracing = true);
        public Task<TEntity> GetQuizWithStats(Guid quizId, bool noTracing = true);
        public Task<TEntity> GetQuizWithAuthorInfo(Guid quizId, bool noTracing = true);
    }
}