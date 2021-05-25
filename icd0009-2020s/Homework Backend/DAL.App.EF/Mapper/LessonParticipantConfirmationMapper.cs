using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mapper
{
    public class LessonParticipantConfirmationMapper : IBaseMapper<DAL.App.DTO.LessonParticipantConfirmation, Domain.App.LessonParticipantConfirmation>
    {
        protected IMapper Mapper;
        public LessonParticipantConfirmationMapper(IMapper mapper)
        {
            Mapper = mapper;
        }
        public virtual DAL.App.DTO.LessonParticipantConfirmation? Map(Domain.App.LessonParticipantConfirmation? inObject)
        {
            return Mapper.Map<DAL.App.DTO.LessonParticipantConfirmation>(inObject);
        }

        public virtual Domain.App.LessonParticipantConfirmation? Map(DAL.App.DTO.LessonParticipantConfirmation? inObject)
        {
            return Mapper.Map<Domain.App.LessonParticipantConfirmation>(inObject);
        }
    }
}