using System;
using Domain.Base;

namespace Domain.App
{
    public class LessonParticipantConfirmation: DomainEntityId
    {
        public bool Confirmation { get; set; } = default!;

        public Guid? LessonParticipantId { get; set; }
        public LessonParticipant? LessonParticipant { get; set; }
    }
}