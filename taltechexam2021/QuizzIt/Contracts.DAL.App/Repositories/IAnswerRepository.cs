using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;


namespace Contracts.DAL.App.Repositories
{
    public interface IAnswerRepository: IBaseRepository<Answer>, IAnswerRepositoryCustom<Answer>
    {
        public Task<Answer> GetAnswerByQuestionAndOrder(Guid questionId, int order, bool noTracing = true);
    }
    // Add User custom method declarations
    public interface IAnswerRepositoryCustom<TEntity>
    {
        public Task<IEnumerable<TEntity>> GetQuestionAnswers(Guid questionId, bool noTracing = true);
        public Task<IEnumerable<TEntity>> GetParticipantAnswers(Guid participantId, bool noTracing = true);
    }
}