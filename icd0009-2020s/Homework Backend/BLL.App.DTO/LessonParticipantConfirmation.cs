using System;
using Domain.Base;

namespace BLL.App.DTO
{
    public class LessonParticipantConfirmation: DomainEntityId
    {
        public bool Confirmation { get; set; } = default!;

        public Guid? LessonParticipantId { get; set; }
        public BLL.App.DTO.LessonParticipant? LessonParticipant { get; set; }
    }
}