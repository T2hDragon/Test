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
using Microsoft.EntityFrameworkCore;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace BLL.App.Services
{
    public class QuestionService: BaseEntityService<IAppUnitOfWork, IQuestionRepository, BLLAppDTO.Question, DALAppDTO.Question>, IQuestionService
    {
        public QuestionService(IAppUnitOfWork serviceUow, IQuestionRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new QuestionMapper(mapper))
        {
        }

        public new async Task<BLLAppDTO.Question> RemoveAsync(Guid id, Guid userId = default)
        {
            var q = await ServiceRepository.RemoveAsync(id, userId);
            var questions = await ServiceRepository.GetQuizQuestions(q.QuizId!.Value);
            questions = questions.Where(question => question.Order > q.Order);
            
            foreach (var question in questions)
            {
                question.Order--;
                ServiceUow.Questions.Update(question);
            }

            await ServiceUow.SaveChangesAsync();
            return Mapper.Map(q)!;
        }

        public async Task<BLLAppDTO.Question> MoveQuestion(Guid questionId, int move, bool noTracing = true)
        {
            var question = await ServiceRepository.FirstOrDefaultAsync(questionId);
            var questionOrder = question!.Order;
            var replacingQuestionOrderMove = move * -1;
            var replacingQuestion = await ServiceRepository.GetQuestionByQuizAndOrder(question!.QuizId!.Value, questionOrder + replacingQuestionOrderMove);
            int replacingQuestionOrder = replacingQuestion!.Order;
            question.Order = replacingQuestionOrder;
            replacingQuestion.Order = questionOrder;
            ServiceUow.Questions.Update(question);
            ServiceUow.Questions.Update(replacingQuestion);
            await ServiceUow.SaveChangesAsync();
            return Mapper.Map(question)!;
        }

        public async Task<IEnumerable<BLLAppDTO.Question>> GetQuizQuestions(Guid quizId, bool noTracing = true)
        {
            return (await ServiceRepository.GetQuizQuestions(quizId, noTracing)).Select(x => Mapper.Map(x))!;

        }
    }
}