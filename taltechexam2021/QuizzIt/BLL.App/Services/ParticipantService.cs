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

namespace BLL.App.Services
{
    public class ParticipantService: BaseEntityService<IAppUnitOfWork, IParticipantRepository, BLLAppDTO.Participant, DALAppDTO.Participant>, IParticipantService
    {
        public ParticipantService(IAppUnitOfWork serviceUow, IParticipantRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new ParticipantMapper(mapper))
        {
        }


        public async Task<double> GetCorrectAnswerPercentage(Guid participantId, bool noTracing = true)
        {
            var participant = await ServiceRepository.FirstOrDefaultAsync(participantId);
            var totalNonPollQuestions = await ServiceUow.Questions.GetQuizNonPollQuestionsTotal(participant!.QuizId);
            var totalParticipantCorrectAnswers = await ServiceUow.Participants.GetCorrectAnswersTotal(participantId);
            return Math.Round(totalParticipantCorrectAnswers / totalNonPollQuestions * 100, 2);
        }

        public async Task<IEnumerable<BLLAppDTO.Participant>> GetUserParticipations(Guid userId, bool noTracing = true)
        {
            return (await ServiceRepository.GetUserParticipations(userId, noTracing)).Select(x => Mapper.Map(x))!;
        }

        public async Task<bool> HasPickedAnswer(Guid participantId, Guid answerId, bool noTracing = true)
        {
            return await ServiceRepository.HasPickedAnswer(participantId, answerId, noTracing);
        }
    }
}