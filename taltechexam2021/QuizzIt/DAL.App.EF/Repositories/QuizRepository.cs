using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mapper;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using DomainQuiz = Domain.App.Quiz;

namespace DAL.App.EF.Repositories
{
    public class QuizRepository : BaseRepository<DAL.App.DTO.Quiz, Domain.App.Quiz, AppDbContext>, IQuizRepository
    {
        public QuizRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new QuizMapper(mapper))
        {
        }


        public async Task<DAL.App.DTO.Quiz> InitializeQuiz(string quizName, Guid creatorId, bool noTracing = true)
        {
            var quiz = await RepoDbSet.AddAsync(new Domain.App.Quiz
            {
                Name = quizName,
                CreatorId = creatorId
            });

            return Mapper.Map(quiz.Entity)!;
        }

        public async Task<Quiz> PublishQuiz(Guid quizId, bool noTracing = true)
        {
            var quiz = await RepoDbSet.FirstAsync(quiz => quiz.Id == quizId);
            quiz.CreatedAt = DateTime.Now;
            quiz = RepoDbSet.Update(quiz).Entity!;
            return Mapper.Map(quiz)!;
        }

        public async Task<IEnumerable<Quiz>> GetUserCreatedQuizzes(Guid creatorId, bool noTracing = true)
        {
            var query = CreateQuery(default, noTracing);

            var queryRes = await query.Where(quiz => quiz.CreatorId == creatorId).ToListAsync();

            return queryRes.Select(x => Mapper.Map(x))!;
        }

        public async Task<IEnumerable<Quiz>> GetQuizzesWithSearchData(Guid searcherId, int page, string search, bool noTracing = true)
        {
            var query = CreateQuery(default, noTracing);

            var queryRes = await query
                .Include(quiz => quiz.Creator)
                .Include(quiz => quiz.Participants)
                .Include(quiz => quiz.Questions)
                .Include(quiz => quiz.Participants)
                .Where(quiz => quiz.Name.Contains(search) & quiz.Questions!.Count > 0 & quiz.Participants!.All(p => p.AppUserId != searcherId))
                .OrderBy(quiz => quiz.Participants!.Count)
                .Skip(page * 5)
                .Take(5)
                .AsSingleQuery()
                .ToListAsync();

            return queryRes.Select(x => Mapper.Map(x))!;
        }

        public async Task<Quiz> GetQuizWithStats(Guid quizId, bool noTracing = true)
        {
            var query = CreateQuery(default, noTracing);

            var queryRes = await query
                .Include(quiz => quiz.Participants)
                .ThenInclude(participant => participant.PickedAnswers)
                .AsSingleQuery()
                .Include(quiz => quiz.Questions!.OrderBy(q => q.Order))
                .ThenInclude(question => question.Answers!.OrderBy(q => q.Order))
                .ThenInclude(answer => answer.PickedAnswers!)
                .AsSingleQuery()
                .FirstAsync(quiz => quiz.Id == quizId);        
            
            return Mapper.Map(queryRes)!;

        }

        public async Task<Quiz> GetQuizWithAuthorInfo(Guid quizId, bool noTracing = true)
        {
            var query = CreateQuery(default, noTracing);

            var queryRes = await query
                .Include(quiz => quiz.Creator)
                .FirstOrDefaultAsync(quiz => quiz.Id == quizId);
            return Mapper.Map(queryRes)!;
        }
    }
}