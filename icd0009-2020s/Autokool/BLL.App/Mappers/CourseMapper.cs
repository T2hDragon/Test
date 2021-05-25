using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class CourseMapper: BaseMapper<BLL.App.DTO.Course, DAL.App.DTO.Course>, IBaseMapper<BLL.App.DTO.Course, DAL.App.DTO.Course>
    {
        public CourseMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}