using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mapper
{
    public class ContractCourseMapper : IBaseMapper<DAL.App.DTO.ContractCourse, Domain.App.ContractCourse>
    {
        protected IMapper Mapper;
        public ContractCourseMapper(IMapper mapper)
        {
            Mapper = mapper;
        }
        public virtual DAL.App.DTO.ContractCourse? Map(Domain.App.ContractCourse? inObject)
        {
            return Mapper.Map<DAL.App.DTO.ContractCourse>(inObject);
        }

        public virtual Domain.App.ContractCourse? Map(DAL.App.DTO.ContractCourse? inObject)
        {
            return Mapper.Map<Domain.App.ContractCourse>(inObject);
        }
    }
}