using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Domain.Base;

namespace BLL.App.DTO
{

    public class Quiz: DomainEntityId
    {
        [MinLength(1)]
        [MaxLength(64)]
        public string Name { get; set; } = default!;
        public DateTime? CreatedAt { get; set; }

        public Guid? CreatorId { get; set; }
        public AppUser? Creator { get; set; } 
        
        public ICollection<BLL.App.DTO.Participant>? Participants { get; set; }
        public ICollection<BLL.App.DTO.Question>? Questions { get; set; }
    }
}