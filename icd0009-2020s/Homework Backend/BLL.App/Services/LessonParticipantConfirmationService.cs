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
    public class LessonParticipantConfirmationService: BaseEntityService<IAppUnitOfWork, ILessonParticipantConfirmationRepository, BLLAppDTO.LessonParticipantConfirmation, DALAppDTO.LessonParticipantConfirmation>, ILessonParticipantConfirmationService
    {
        public LessonParticipantConfirmationService(IAppUnitOfWork serviceUow, ILessonParticipantConfirmationRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new LessonParticipantConfirmationMapper(mapper))
        {
        }
    }
}