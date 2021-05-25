using System;
using Domain.Base;

namespace BLL.App.DTO
{

    public class PickedAnswer: DomainEntityId
    {
        public Guid ParticipantId { get; set; } = default!;
        public BLL.App.DTO.Participant Participant { get; set; } = default!;
        
        public Guid AnswerId { get; set; } = default!;
        public BLL.App.DTO.Answer Answer { get; set; } = default!;

    }
}