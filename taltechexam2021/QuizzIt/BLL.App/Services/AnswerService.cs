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
    public class AnswerService: BaseEntityService<IAppUnitOfWork, IAnswerRepository, BLLAppDTO.Answer, DALAppDTO.Answer>, IAnswerService
    {
        public AnswerService(IAppUnitOfWork serviceUow, IAnswerRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new AnswerMapper(mapper))
        {
        }

        public new async Task<BLLAppDTO.Answer> RemoveAsync(Guid id, Guid userId = default)
        {
            var a = await ServiceRepository.RemoveAsync(id, userId);
            var answers = await ServiceRepository.GetQuestionAnswers(a.QuestionId!.Value);
            answers = answers.Where(answer => answer.Order > a.Order);
            
            foreach (var answer in answers)
            {
                answer.Order--;
                ServiceUow.Answers.Update(answer);
            }
            await ServiceUow.SaveChangesAsync();
            return Mapper.Map(a)!;
        }
        public async Task<BLLAppDTO.Answer> MoveAnswer(Guid answerId, int move, bool noTracing = true)
        {
            var answer = await ServiceRepository.FirstOrDefaultAsync(answerId);
            var answerOrder = answer!.Order;
            var replacingAnswer = await ServiceRepository.GetAnswerByQuestionAndOrder(answer!.QuestionId!.Value, answerOrder + move * -1);
            int replacingAnswerOrder = replacingAnswer!.Order;
            answer.Order = replacingAnswerOrder;
            replacingAnswer.Order = answerOrder;
            ServiceUow.Answers.Update(answer);
            ServiceUow.Answers.Update(replacingAnswer);
            await ServiceUow.SaveChangesAsync();
            return Mapper.Map(answer)!;
        }
        

        public async Task SetCorrectAnswer(Guid answerId, bool noTracing = true)
        {
            var a = await ServiceRepository.FirstOrDefaultAsync(answerId);
            var answers = (await ServiceRepository.GetQuestionAnswers(a!.QuestionId!.Value, noTracing)).Select(x => Mapper.Map(x)!);

            foreach (var answer in answers)
            {
                if (answer.IsCorrect)
                {
                    answer.IsCorrect = false;
                    ServiceUow.Answers.Update(Mapper.Map(answer)!);
                    await ServiceUow.SaveChangesAsync();
                }
                if (answer.Id == answerId)
                {
                    answer.IsCorrect = true;
                    ServiceUow.Answers.Update(Mapper.Map(answer)!);
                    await ServiceUow.SaveChangesAsync();
                }
            }
        }

        public async Task SetAnswerCorrect(Guid answerId, bool correct, bool noTracing = true)
        {
            var answer = await ServiceRepository.FirstOrDefaultAsync(answerId);
            answer!.IsCorrect = correct;
            ServiceUow.Answers.Update(answer);
            await ServiceUow.SaveChangesAsync();        
        }

        public async Task<IEnumerable<BLLAppDTO.Answer>> GetQuestionAnswers(Guid questionId, bool noTracing = true)
        {
            return (await ServiceRepository.GetQuestionAnswers(questionId, noTracing)).Select(x => Mapper.Map(x)!);
        }

        public async Task<IEnumerable<BLLAppDTO.Answer>> GetParticipantAnswers(Guid participantId, bool noTracing = true)
        {
            return (await ServiceRepository.GetParticipantAnswers(participantId, noTracing)).Select(x => Mapper.Map(x)!);
        }
    }
}