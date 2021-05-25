using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mapper
{
    public class AnswerMapper : IBaseMapper<DAL.App.DTO.Answer, Domain.App.Answer>
    {
        protected IMapper Mapper;
        public AnswerMapper(IMapper mapper)
        {
            Mapper = mapper;
        }
        public virtual DAL.App.DTO.Answer? Map(Domain.App.Answer? inObject)
        {
            return Mapper.Map<DAL.App.DTO.Answer>(inObject);
        }

        public virtual Domain.App.Answer? Map(DAL.App.DTO.Answer? inObject)
        {
            return Mapper.Map<Domain.App.Answer>(inObject);
        }
    }
}