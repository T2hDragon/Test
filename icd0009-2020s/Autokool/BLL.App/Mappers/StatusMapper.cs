using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class StatusMapper: BaseMapper<BLL.App.DTO.Status, DAL.App.DTO.Status>, IBaseMapper<BLL.App.DTO.Status, DAL.App.DTO.Status>
    {
        public StatusMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}