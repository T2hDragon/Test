using AutoMapper;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base.Repositories;
using DAL.App.EF.Mappers;
using DAL.App.EF.Repositories;
using DAL.Base.EF;
using DAL.Base.EF.Repositories;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;

namespace DAL.App.EF
{
    public class AppUnitOfWork : BaseUnitOfWork<AppDbContext>, IAppUnitOfWork
    {
        protected IMapper Mapper;
        public AppUnitOfWork(AppDbContext uowDbContext, IMapper mapper) : base(uowDbContext)
        {
            Mapper = mapper;
        }
        
        public IAnswerRepository Answers => GetRepository(() => new AnswerRepository(UowDbContext, Mapper));
        public IParticipantRepository Participants => GetRepository(() => new ParticipantRepository(UowDbContext, Mapper));
        public IPickedAnswerRepository PickedAnswers => GetRepository(() => new PickedAnswerRepository(UowDbContext, Mapper));
        public IQuestionRepository Questions => GetRepository(() => new QuestionRepository(UowDbContext, Mapper));
        public IQuizRepository Quizzes => GetRepository(() => new QuizRepository(UowDbContext, Mapper));
        
        
    }
    
}