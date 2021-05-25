using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class ParticipantMapper: BaseMapper<BLL.App.DTO.Participant, DAL.App.DTO.Participant>, IBaseMapper<BLL.App.DTO.Participant, DAL.App.DTO.Participant>
    {
        public ParticipantMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}