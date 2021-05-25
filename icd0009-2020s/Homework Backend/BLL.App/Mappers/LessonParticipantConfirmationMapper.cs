using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class LessonParticipantConfirmationMapper: BaseMapper<BLL.App.DTO.LessonParticipantConfirmation, DAL.App.DTO.LessonParticipantConfirmation>, IBaseMapper<BLL.App.DTO.LessonParticipantConfirmation, DAL.App.DTO.LessonParticipantConfirmation>
    {
        public LessonParticipantConfirmationMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}