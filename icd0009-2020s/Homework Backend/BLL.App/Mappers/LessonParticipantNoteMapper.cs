using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class LessonParticipantNoteMapper: BaseMapper<BLL.App.DTO.LessonParticipantNote, DAL.App.DTO.LessonParticipantNote>, IBaseMapper<BLL.App.DTO.LessonParticipantNote, DAL.App.DTO.LessonParticipantNote>
    {
        public LessonParticipantNoteMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}