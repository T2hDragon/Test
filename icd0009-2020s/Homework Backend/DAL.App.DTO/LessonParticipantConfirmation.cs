using System;
using Domain.Base;

namespace DAL.App.DTO
{
    public class LessonParticipantConfirmation: DomainEntityId
    {
        public bool Confirmation { get; set; } = default!;

        public Guid? LessonParticipantId { get; set; }
        public DAL.App.DTO.LessonParticipant? LessonParticipant { get; set; }
    }
}