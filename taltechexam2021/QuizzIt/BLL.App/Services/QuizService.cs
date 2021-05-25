using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;
using DomainQuiz = Domain.App.Quiz;

namespace BLL.App.Services
{
    public class QuizService: BaseEntityService<IAppUnitOfWork, IQuizRepository, BLLAppDTO.Quiz, DALAppDTO.Quiz>, IQuizService
    {
        public QuizService(IAppUnitOfWork serviceUow, IQuizRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new QuizMapper(mapper))
        {
        }


        public async Task<BLLAppDTO.Quiz> InitializeQuiz(string quizName, Guid creatorId, bool noTracing = true)
        {
            return Mapper.Map(await ServiceRepository.InitializeQuiz(quizName, creatorId, noTracing))!;
        }

        public async Task<BLLAppDTO.Quiz> PublishQuiz(Guid quizId, bool noTracing = true)
        {
            return Mapper.Map(await ServiceRepository.PublishQuiz(quizId, noTracing))!;
        }

        public async Task<IEnumerable<BLLAppDTO.Quiz>> GetUserCreatedQuizzes(Guid creatorId, bool noTracing = true)
        {
            return (await ServiceRepository.GetUserCreatedQuizzes(creatorId, noTracing)).Select(x => Mapper.Map(x))!;
        }

        public async Task<IEnumerable<BLLAppDTO.Quiz>> GetQuizzesWithSearchData(Guid searcherId, int page, string search, bool noTracing = true)
        {
            return (await ServiceRepository.GetQuizzesWithSearchData(searcherId, page, search, noTracing)).Select(x => Mapper.Map(x))!;
        }

        public async Task<BLLAppDTO.Quiz> GetQuizWithStats(Guid quizId, bool noTracing = true)
        {
            return Mapper.Map(await ServiceRepository.GetQuizWithStats(quizId, noTracing))!;
        }

        public async Task<BLLAppDTO.Quiz> GetQuizWithAuthorInfo(Guid quizId, bool noTracing = true)
        {
            return Mapper.Map(await ServiceRepository.GetQuizWithAuthorInfo(quizId, noTracing))!;
        }
    }
}