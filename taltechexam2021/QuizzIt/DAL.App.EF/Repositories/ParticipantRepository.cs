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
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Participant = DAL.App.DTO.Participant;
using PickedAnswer = BLL.App.DTO.PickedAnswer;

namespace DAL.App.EF.Repositories
{
    public class ParticipantRepository : BaseRepository<DAL.App.DTO.Participant, Domain.App.Participant, AppDbContext>, IParticipantRepository
    {
        public ParticipantRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new ParticipantMapper(mapper))
        {
        }


        public async Task<double> GetCorrectAnswersTotal(Guid participantId, bool noTracing = true)
        {
            var query = CreateQuery(default, noTracing);
            var total = (await query
                        .Include(participant => participant.PickedAnswers)
                        .ThenInclude(answer => answer.Answer)
                        .FirstOrDefaultAsync(participant => participant.Id == participantId))
                    .PickedAnswers!.Select(pickedAnswer => pickedAnswer.Answer)
                    .Count(answer => answer.IsCorrect);
            return total;
        }

        public async Task<IEnumerable<Participant>> GetUserParticipations(Guid userId, bool noTracing = true)
        {
            var query = CreateQuery(default, noTracing);
            var participations = await query
                .Include(participant => participant.PickedAnswers)
                .ThenInclude(pickedAnswer => pickedAnswer.Answer)
                .Where(participant => participant.AppUserId == userId)
                .ToListAsync();
            return participations.Select(x => Mapper.Map(x))!;
        }

        public async Task<bool> HasPickedAnswer(Guid participantId, Guid answerId, bool noTracing = true)
        {
            var query = CreateQuery(default, noTracing);

            var result = await query
                .Include(participant => participant.PickedAnswers)
                .AnyAsync(participant =>
                    participant.PickedAnswers!.Any(pickedAnswer =>
                        pickedAnswer.AnswerId == answerId));
            return result;
        }
    }
}