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
using Answer = DAL.App.DTO.Answer;

namespace DAL.App.EF.Repositories
{
    public class AnswerRepository : BaseRepository<DAL.App.DTO.Answer, Domain.App.Answer, AppDbContext>, IAnswerRepository
    {
        public AnswerRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new AnswerMapper(mapper))
        {
        }


        public async Task<IEnumerable<Answer>> GetQuestionAnswers(Guid questionId, bool noTracing = true)
        {
            var query = CreateQuery(default, noTracing);

            var answers = await query
                .Include(question => question.PickedAnswers)
                .Where(q => q.QuestionId == questionId)
                .OrderBy(q => q.Order)
                .ToListAsync();

            return answers.Select(x => Mapper.Map(x)!)!;
        }

        public async Task<IEnumerable<Answer>> GetParticipantAnswers(Guid participantId, bool noTracing = true)
        {
            var query = CreateQuery(default, noTracing);

            var answers = await query
                .Include(answer => answer.PickedAnswers)
                .Where(answer => answer.PickedAnswers!.Any(pickedAnswer => pickedAnswer.ParticipantId == participantId))
                .ToListAsync();

            return answers.Select(x => Mapper.Map(x)!);
        }

        public async Task<Answer> GetAnswerByQuestionAndOrder(Guid questionId, int order, bool noTracing = true)
        {

            var query = CreateQuery(default, noTracing);

            var question = await query
                .FirstAsync(q => q.QuestionId == questionId & q.Order == order);

            return Mapper.Map(question)!;
                
        }        
    }
}