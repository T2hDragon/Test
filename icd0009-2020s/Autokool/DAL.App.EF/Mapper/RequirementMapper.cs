using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mapper
{
    public class RequirementMapper : IBaseMapper<DAL.App.DTO.Requirement, Domain.App.Requirement>
    {
        protected IMapper Mapper;
        public RequirementMapper(IMapper mapper)
        {
            Mapper = mapper;
        }
        public virtual DAL.App.DTO.Requirement? Map(Domain.App.Requirement? inObject)
        {
            return Mapper.Map<DAL.App.DTO.Requirement>(inObject);
        }

        public virtual Domain.App.Requirement? Map(DAL.App.DTO.Requirement? inObject)
        {
            return Mapper.Map<Domain.App.Requirement>(inObject);
        }
    }
}