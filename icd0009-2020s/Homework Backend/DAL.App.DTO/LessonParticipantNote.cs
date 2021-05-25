using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace DAL.App.DTO
{
    public class LessonParticipantNote: DomainEntityId
    {
        [MinLength(1)]
        [MaxLength(256)]
        public string Note { get; set; } = default!;
        
        
        public Guid? LessonParticipantId { get; set; }
        public DAL.App.DTO.LessonParticipant? LessonParticipant { get; set; }
    }
}