using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;


namespace Contracts.DAL.App.Repositories
{
    public interface IParticipantRepository: IBaseRepository<Participant>, IParticipantRepositoryCustom<Participant>
    {
        public Task<double> GetCorrectAnswersTotal(Guid participantId, bool noTracing = true);

    }
    // Add User custom method declarations
    public interface IParticipantRepositoryCustom<TEntity>
    {
        public Task<IEnumerable<TEntity>> GetUserParticipations(Guid userId, bool noTracing = true);
        public Task<bool> HasPickedAnswer(Guid participantId, Guid answerId, bool noTracing = true);
    }
}