using System;
using System.Collections.Generic;
using Domain.App.Identity;
using Domain.Base;

namespace DAL.App.DTO
{

    public class Participant: DomainEntityId
    {
        public Guid? AppUserId { get; set; } 
        public AppUser? AppUser { get; set; } 
        
        public Guid QuizId { get; set; } = default!;
        public DAL.App.DTO.Quiz Quiz { get; set; } = default!;
        
        public ICollection<DAL.App.DTO.PickedAnswer>? PickedAnswers { get; set; }

    }
}