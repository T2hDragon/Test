using AutoMapper;
using BLL.App.Mappers;
using BLL.App.Services;
using BLL.Base;
using BLL.Base.Services;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App;
using Contracts.DAL.Base.Repositories;

namespace BLL.App
{
    public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
    {
        protected IMapper Mapper;
        public AppBLL(IAppUnitOfWork uow, IMapper mapper) : base(uow)
        {
            Mapper = mapper;
        }
        public IAnswerService Answers =>
            GetService<IAnswerService>(() => new AnswerService(Uow, Uow.Answers, Mapper));
        public IParticipantService Participants =>
            GetService<IParticipantService>(() => new ParticipantService(Uow, Uow.Participants, Mapper));
        public IPickedAnswerService PickedAnswers =>
            GetService<IPickedAnswerService>(() => new PickedAnswerService(Uow, Uow.PickedAnswers, Mapper));
        public IQuestionService Questions =>
            GetService<IQuestionService>(() => new QuestionService(Uow, Uow.Questions, Mapper));
        public IQuizService Quizzes =>
            GetService<IQuizService>(() => new QuizService(Uow, Uow.Quizzes, Mapper));
    }
}