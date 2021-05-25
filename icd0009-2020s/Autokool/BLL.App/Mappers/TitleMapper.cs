using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class TitleMapper: BaseMapper<BLL.App.DTO.Title, DAL.App.DTO.Title>, IBaseMapper<BLL.App.DTO.Title, DAL.App.DTO.Title>
    {
        public TitleMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}