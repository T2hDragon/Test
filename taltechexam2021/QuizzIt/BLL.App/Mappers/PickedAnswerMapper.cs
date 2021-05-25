using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class PickedAnswerMapper: BaseMapper<BLL.App.DTO.PickedAnswer, DAL.App.DTO.PickedAnswer>, IBaseMapper<BLL.App.DTO.PickedAnswer, DAL.App.DTO.PickedAnswer>
    {
        public PickedAnswerMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}