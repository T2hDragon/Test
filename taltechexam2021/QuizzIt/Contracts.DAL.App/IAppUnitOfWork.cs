using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork
    {

        IAnswerRepository Answers { get; }
        IParticipantRepository Participants { get; }
        IPickedAnswerRepository PickedAnswers { get; }
        IQuestionRepository Questions { get; }
        IQuizRepository Quizzes { get; }
    }
}