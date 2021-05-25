using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class CourseRequirementMapper: BaseMapper<BLL.App.DTO.CourseRequirement, DAL.App.DTO.CourseRequirement>, IBaseMapper<BLL.App.DTO.CourseRequirement, DAL.App.DTO.CourseRequirement>
    {
        public CourseRequirementMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}