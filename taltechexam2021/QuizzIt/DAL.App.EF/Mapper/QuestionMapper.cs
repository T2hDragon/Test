using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mapper
{
    public class QuestionMapper : IBaseMapper<DAL.App.DTO.Question, Domain.App.Question>
    {
        protected IMapper Mapper;
        public QuestionMapper(IMapper mapper)
        {
            Mapper = mapper;
        }
        public virtual DAL.App.DTO.Question? Map(Domain.App.Question? inObject)
        {
            return Mapper.Map<DAL.App.DTO.Question>(inObject);
        }

        public virtual Domain.App.Question? Map(DAL.App.DTO.Question? inObject)
        {
            return Mapper.Map<Domain.App.Question>(inObject);
        }
        
    }
}