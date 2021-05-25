using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mapper
{
    public class ContactMapper : IBaseMapper<DAL.App.DTO.Contact, Domain.App.Contact>
    {
        protected IMapper Mapper;
        public ContactMapper(IMapper mapper)
        {
            Mapper = mapper;
        }
        public virtual DAL.App.DTO.Contact? Map(Domain.App.Contact? inObject)
        {
            return Mapper.Map<DAL.App.DTO.Contact>(inObject);
        }

        public virtual Domain.App.Contact? Map(DAL.App.DTO.Contact? inObject)
        {
            return Mapper.Map<Domain.App.Contact>(inObject);
        }
    }
}