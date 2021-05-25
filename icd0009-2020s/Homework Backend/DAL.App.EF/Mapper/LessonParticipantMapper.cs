using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mapper
{
    public class LessonParticipantMapper : IBaseMapper<DAL.App.DTO.LessonParticipant, Domain.App.LessonParticipant>
    {
        protected IMapper Mapper;
        public LessonParticipantMapper(IMapper mapper)
        {
            Mapper = mapper;
        }
        public virtual DAL.App.DTO.LessonParticipant? Map(Domain.App.LessonParticipant? inObject)
        {
            return Mapper.Map<DAL.App.DTO.LessonParticipant>(inObject);
        }

        public virtual Domain.App.LessonParticipant? Map(DAL.App.DTO.LessonParticipant? inObject)
        {
            return Mapper.Map<Domain.App.LessonParticipant>(inObject);
        }
    }
}