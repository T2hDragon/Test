using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mapper
{
    public class ContractMapper : IBaseMapper<DAL.App.DTO.Contract, Domain.App.Contract>
    {
        protected IMapper Mapper;
        public ContractMapper(IMapper mapper)
        {
            Mapper = mapper;
        }
        public virtual DAL.App.DTO.Contract? Map(Domain.App.Contract? inObject)
        {
            return Mapper.Map<DAL.App.DTO.Contract>(inObject);
        }

        public virtual Domain.App.Contract? Map(DAL.App.DTO.Contract? inObject)
        {
            return Mapper.Map<Domain.App.Contract>(inObject);
        }
    }
}