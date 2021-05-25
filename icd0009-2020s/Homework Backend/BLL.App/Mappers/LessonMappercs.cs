using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class LessonMapper: BaseMapper<BLL.App.DTO.Lesson, DAL.App.DTO.Lesson>, IBaseMapper<BLL.App.DTO.Lesson, DAL.App.DTO.Lesson>
    {
        public LessonMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}