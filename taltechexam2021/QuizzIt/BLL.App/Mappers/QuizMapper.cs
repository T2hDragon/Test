using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class QuizMapper: BaseMapper<BLL.App.DTO.Quiz, DAL.App.DTO.Quiz>, IBaseMapper<BLL.App.DTO.Quiz, DAL.App.DTO.Quiz>
    {
        public QuizMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}