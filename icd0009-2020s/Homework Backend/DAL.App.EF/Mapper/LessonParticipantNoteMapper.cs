using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mapper
{
    public class LessonParticipantNoteMapper : IBaseMapper<DAL.App.DTO.LessonParticipantNote, Domain.App.LessonParticipantNote>
    {
        protected IMapper Mapper;
        public LessonParticipantNoteMapper(IMapper mapper)
        {
            Mapper = mapper;
        }
        public virtual DAL.App.DTO.LessonParticipantNote? Map(Domain.App.LessonParticipantNote? inObject)
        {
            return Mapper.Map<DAL.App.DTO.LessonParticipantNote>(inObject);
        }

        public virtual Domain.App.LessonParticipantNote? Map(DAL.App.DTO.LessonParticipantNote? inObject)
        {
            return Mapper.Map<Domain.App.LessonParticipantNote>(inObject);
        }
    }
}