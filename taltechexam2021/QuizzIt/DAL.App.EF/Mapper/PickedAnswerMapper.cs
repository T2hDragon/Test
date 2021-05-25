using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mapper
{
    public class PickedAnswerMapper : IBaseMapper<DAL.App.DTO.PickedAnswer, Domain.App.PickedAnswer>
    {
        protected IMapper Mapper;
        public PickedAnswerMapper(IMapper mapper)
        {
            Mapper = mapper;
        }
        public virtual DAL.App.DTO.PickedAnswer? Map(Domain.App.PickedAnswer? inObject)
        {
            return Mapper.Map<DAL.App.DTO.PickedAnswer>(inObject);
        }

        public virtual Domain.App.PickedAnswer? Map(DAL.App.DTO.PickedAnswer? inObject)
        {
            return Mapper.Map<Domain.App.PickedAnswer>(inObject);
        }
    }
}