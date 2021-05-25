using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mapper
{
    public class CourseMapper : IBaseMapper<DAL.App.DTO.Course, Domain.App.Course>
    {
        protected IMapper Mapper;
        public CourseMapper(IMapper mapper)
        {
            Mapper = mapper;
        }
        public virtual DAL.App.DTO.Course? Map(Domain.App.Course? inObject)
        {
            return Mapper.Map<DAL.App.DTO.Course>(inObject);
        }

        public virtual Domain.App.Course? Map(DAL.App.DTO.Course? inObject)
        {
            return Mapper.Map<Domain.App.Course>(inObject);
        }
    }
}