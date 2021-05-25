using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;


namespace Contracts.DAL.App.Repositories
{
    public interface IQuestionRepository: IBaseRepository<Question>, IQuestionRepositoryCustom<Question>
    {
        public Task<Question> GetQuestionByQuizAndOrder(Guid quizId, int order, bool noTracing = true);
        public Task<double> GetQuizNonPollQuestionsTotal(Guid quizId, bool noTracing = true);

    }
    // Add User custom method declarations
    public interface IQuestionRepositoryCustom<TEntity>
    {
        public Task<IEnumerable<TEntity>> GetQuizQuestions(Guid quizId, bool noTracing = true);

    }
}