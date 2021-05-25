using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mapper
{
    public class TitleMapper : IBaseMapper<DAL.App.DTO.Title, Domain.App.Title>
    {
        protected IMapper Mapper;
        public TitleMapper(IMapper mapper)
        {
            Mapper = mapper;
        }
        public virtual DAL.App.DTO.Title? Map(Domain.App.Title? inObject)
        {
            return Mapper.Map<DAL.App.DTO.Title>(inObject);
        }

        public virtual Domain.App.Title? Map(DAL.App.DTO.Title? inObject)
        {
            return Mapper.Map<Domain.App.Title>(inObject);
        }
    }
}