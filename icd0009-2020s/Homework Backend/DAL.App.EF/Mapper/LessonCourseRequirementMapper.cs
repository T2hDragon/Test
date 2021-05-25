using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mapper
{
    public class LessonCourseRequirementMapper : IBaseMapper<DAL.App.DTO.LessonCourseRequirement, Domain.App.LessonCourseRequirement>
    {
        protected IMapper Mapper;
        public LessonCourseRequirementMapper(IMapper mapper)
        {
            Mapper = mapper;
        }
        public virtual DAL.App.DTO.LessonCourseRequirement? Map(Domain.App.LessonCourseRequirement? inObject)
        {
            return Mapper.Map<DAL.App.DTO.LessonCourseRequirement>(inObject);
        }

        public virtual Domain.App.LessonCourseRequirement? Map(DAL.App.DTO.LessonCourseRequirement? inObject)
        {
            return Mapper.Map<Domain.App.LessonCourseRequirement>(inObject);
        }
    }
}