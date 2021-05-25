using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;
namespace Contracts.BLL.App.Services
{
    public interface ILessonParticipantNoteService: IBaseEntityService<BLLAppDTO.LessonParticipantNote, DALAppDTO.LessonParticipantNote>, ILessonParticipantNoteRepositoryCustom<BLLAppDTO.LessonParticipantNote>
    {

    }
}