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
    public class PickedAnswerService: BaseEntityService<IAppUnitOfWork, IPickedAnswerRepository, BLLAppDTO.PickedAnswer, DALAppDTO.PickedAnswer>, IPickedAnswerService
    {
        public PickedAnswerService(IAppUnitOfWork serviceUow, IPickedAnswerRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new PickedAnswerMapper(mapper))
        {
        }


        public async Task Submit(Guid participantId, Guid answerId, bool noTracing = true)
        {
            ServiceUow.PickedAnswers.Add(new DALAppDTO.PickedAnswer
            {
                AnswerId = answerId,
                ParticipantId = participantId
            });
            await ServiceUow.SaveChangesAsync();
        }
    }
}