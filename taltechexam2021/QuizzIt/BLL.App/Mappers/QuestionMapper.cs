using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class QuestionMapper: BaseMapper<BLL.App.DTO.Question, DAL.App.DTO.Question>, IBaseMapper<BLL.App.DTO.Question, DAL.App.DTO.Question>
    {
        public QuestionMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}