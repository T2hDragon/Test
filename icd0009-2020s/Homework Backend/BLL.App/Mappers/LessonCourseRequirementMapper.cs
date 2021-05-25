using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class LessonCourseRequirementMapper: BaseMapper<BLL.App.DTO.LessonCourseRequirement, DAL.App.DTO.LessonCourseRequirement>, IBaseMapper<BLL.App.DTO.LessonCourseRequirement, DAL.App.DTO.LessonCourseRequirement>
    {
        public LessonCourseRequirementMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}