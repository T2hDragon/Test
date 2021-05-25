using System;
using Domain.App;
using Domain.Base;

namespace DAL.App.DTO
{

    public class PickedAnswer: DomainEntityId
    {
        public Guid ParticipantId { get; set; } = default!;
        public Participant Participant { get; set; } = default!;
        
        public Guid AnswerId { get; set; } = default!;
        public Answer Answer { get; set; } = default!;

    }
}