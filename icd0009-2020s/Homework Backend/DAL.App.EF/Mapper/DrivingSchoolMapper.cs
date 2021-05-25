using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mapper
{
    public class DrivingSchoolMapper : IBaseMapper<DAL.App.DTO.DrivingSchool, Domain.App.DrivingSchool>
    {
        protected IMapper Mapper;
        public DrivingSchoolMapper(IMapper mapper)
        {
            Mapper = mapper;
        }
        public virtual DAL.App.DTO.DrivingSchool? Map(Domain.App.DrivingSchool? inObject)
        {
            return Mapper.Map<DAL.App.DTO.DrivingSchool>(inObject);
        }

        public virtual Domain.App.DrivingSchool? Map(DAL.App.DTO.DrivingSchool? inObject)
        {
            return Mapper.Map<Domain.App.DrivingSchool>(inObject);
        }
    }
}