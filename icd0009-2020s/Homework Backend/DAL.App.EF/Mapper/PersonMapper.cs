using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mapper
{
    public class PersonMapper : IBaseMapper<DAL.App.DTO.Person, Domain.App.Person>
    {
        protected IMapper Mapper;
        public PersonMapper(IMapper mapper)
        {
            Mapper = mapper;
        }
        public virtual DAL.App.DTO.Person? Map(Domain.App.Person? inObject)
        {
            return Mapper.Map<DAL.App.DTO.Person>(inObject);
        }

        public virtual Domain.App.Person? Map(DAL.App.DTO.Person? inObject)
        {
            return Mapper.Map<Domain.App.Person>(inObject);
        }
    }
}