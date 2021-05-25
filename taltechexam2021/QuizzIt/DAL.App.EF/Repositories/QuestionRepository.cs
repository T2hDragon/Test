using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mapper;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;
using Question = DAL.App.DTO.Question;

namespace DAL.App.EF.Repositories
{
    public class QuestionRepository : BaseRepository<DAL.App.DTO.Question, Domain.App.Question, AppDbContext>, IQuestionRepository
    {
        public QuestionRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new QuestionMapper(mapper))
        {
        }


        public override async Task<DAL.App.DTO.Question?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);

            var question = await query
                .Include(q => q.Answers)
                .FirstAsync(q => q.Id == id);

            return Mapper.Map(question)!;        }

        public async Task<DAL.App.DTO.Question> GetQuestionByQuizAndOrder(Guid quizId, int order, bool noTracing = true)
        {
            var query = CreateQuery(default, noTracing);

            var question = await query
                .FirstAsync(q => q.QuizId == quizId & q.Order == order);

            return Mapper.Map(question)!;
        }

        public async Task<double> GetQuizNonPollQuestionsTotal(Guid quizId, bool noTracing = true)
        {
            var query = CreateQuery(default, noTracing);

            var total = await query
                .Include(question => question.Answers)
                .CountAsync(question => question.QuizId == quizId & question.Answers!.Any(answer => answer.IsCorrect));
            return total;
        }

        public async Task<IEnumerable<Question>> GetQuizQuestions(Guid quizId, bool noTracing = true)
        {
            var query = CreateQuery(default, noTracing);

            var questions = await query
                .Include(question => question.Answers)
                .Where(q => q.QuizId == quizId)
                .OrderBy(q => q.Order)
                .ToListAsync();

            return questions.Select(x => Mapper.Map(x)!);
        }
    }
}