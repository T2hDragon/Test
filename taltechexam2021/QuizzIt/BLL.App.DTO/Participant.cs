using System;
using System.Collections.Generic;
using Domain.App.Identity;
using Domain.Base;

namespace BLL.App.DTO
{

    public class Participant: DomainEntityId
    {
        public Guid? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        
        public Guid QuizId { get; set; } = default!;
        public Quiz Quiz { get; set; } = default!;
        
        public ICollection<PickedAnswer>? PickedAnswers { get; set; }

    }
}