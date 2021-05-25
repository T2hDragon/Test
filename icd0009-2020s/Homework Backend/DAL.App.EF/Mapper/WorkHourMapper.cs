using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mapper
{
    public class WorkHourMapper : IBaseMapper<DAL.App.DTO.WorkHour, Domain.App.WorkHour>
    {
        protected IMapper Mapper;
        public WorkHourMapper(IMapper mapper)
        {
            Mapper = mapper;
        }
        public virtual DAL.App.DTO.WorkHour? Map(Domain.App.WorkHour? inObject)
        {
            return Mapper.Map<DAL.App.DTO.WorkHour>(inObject);
        }

        public virtual Domain.App.WorkHour? Map(DAL.App.DTO.WorkHour? inObject)
        {
            return Mapper.Map<Domain.App.WorkHour>(inObject);
        }
    }
}