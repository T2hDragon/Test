using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class WorkHourMapper: BaseMapper<BLL.App.DTO.WorkHour, DAL.App.DTO.WorkHour>, IBaseMapper<BLL.App.DTO.WorkHour, DAL.App.DTO.WorkHour>
    {
        public WorkHourMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}