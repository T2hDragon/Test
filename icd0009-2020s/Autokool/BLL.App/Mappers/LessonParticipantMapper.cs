using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class LessonParticipantMapper: BaseMapper<BLL.App.DTO.LessonParticipant, DAL.App.DTO.LessonParticipant>, IBaseMapper<BLL.App.DTO.LessonParticipant, DAL.App.DTO.LessonParticipant>
    {
        public LessonParticipantMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}