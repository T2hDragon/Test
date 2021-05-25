using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mapper
{
    public class ParticipantMapper : IBaseMapper<DAL.App.DTO.Participant, Domain.App.Participant>
    {
        protected IMapper Mapper;
        public ParticipantMapper(IMapper mapper)
        {
            Mapper = mapper;
        }
        public virtual DAL.App.DTO.Participant? Map(Domain.App.Participant? inObject)
        {
            return Mapper.Map<DAL.App.DTO.Participant>(inObject);
        }

        public virtual Domain.App.Participant? Map(DAL.App.DTO.Participant? inObject)
        {
            return Mapper.Map<Domain.App.Participant>(inObject);
        }
    }
}