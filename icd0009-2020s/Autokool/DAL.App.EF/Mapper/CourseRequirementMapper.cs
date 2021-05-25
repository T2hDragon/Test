using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mapper
{
    public class CourseRequirementMapper : IBaseMapper<DAL.App.DTO.CourseRequirement, Domain.App.CourseRequirement>
    {
        protected IMapper Mapper;
        public CourseRequirementMapper(IMapper mapper)
        {
            Mapper = mapper;
        }
        public virtual DAL.App.DTO.CourseRequirement? Map(Domain.App.CourseRequirement? inObject)
        {
            return Mapper.Map<DAL.App.DTO.CourseRequirement>(inObject);
        }

        public virtual Domain.App.CourseRequirement? Map(DAL.App.DTO.CourseRequirement? inObject)
        {
            return Mapper.Map<Domain.App.CourseRequirement>(inObject);
        }
    }
}