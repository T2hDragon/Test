using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mapper
{
    public class LessonMapper : IBaseMapper<DAL.App.DTO.Lesson, Domain.App.Lesson>
    {
        protected IMapper Mapper;
        public LessonMapper(IMapper mapper)
        {
            Mapper = mapper;
        }
        public virtual DAL.App.DTO.Lesson? Map(Domain.App.Lesson? inObject)
        {
            return Mapper.Map<DAL.App.DTO.Lesson>(inObject);
        }

        public virtual Domain.App.Lesson? Map(DAL.App.DTO.Lesson? inObject)
        {
            return Mapper.Map<Domain.App.Lesson>(inObject);
        }
    }
}