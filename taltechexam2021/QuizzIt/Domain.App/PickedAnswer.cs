using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{

    public class PickedAnswer: DomainEntityId
    {
        public Guid ParticipantId { get; set; } = default!;
        public Participant Participant { get; set; } = default!;
        
        public Guid AnswerId { get; set; } = default!;
        public Answer Answer { get; set; } = default!;

    }
}