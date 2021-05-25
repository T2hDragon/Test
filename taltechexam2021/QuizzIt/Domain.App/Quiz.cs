﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Domain.Base;
using Resources.BLL.App.DTO;

namespace Domain.App
{

    public class Quiz: DomainEntityId
    {
        [MinLength(1)]
        [MaxLength(64)]
        public string Name { get; set; } = default!;
        public DateTime? CreatedAt { get; set; }

        public Guid CreatorId { get; set; } = default!;
        public AppUser Creator { get; set; } = default!;
        
        public ICollection<Participant>? Participants { get; set; }
        public ICollection<Question>? Questions { get; set; }
    }
}