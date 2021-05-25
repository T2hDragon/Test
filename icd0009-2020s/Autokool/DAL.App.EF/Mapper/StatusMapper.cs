using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mapper
{
    public class StatusMapper : IBaseMapper<DAL.App.DTO.Status, Domain.App.Status>
    {
        protected IMapper Mapper;
        public StatusMapper(IMapper mapper)
        {
            Mapper = mapper;
        }
        public virtual DAL.App.DTO.Status? Map(Domain.App.Status? inObject)
        {
            return Mapper.Map<DAL.App.DTO.Status>(inObject);
        }

        public virtual Domain.App.Status? Map(DAL.App.DTO.Status? inObject)
        {
            return Mapper.Map<Domain.App.Status>(inObject);
        }
    }
}