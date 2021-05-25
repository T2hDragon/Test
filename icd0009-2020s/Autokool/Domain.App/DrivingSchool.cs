using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class DrivingSchool: DomainEntityId
    {
        public Guid NameId { get; set; }

        [MinLength(1)]
        [MaxLength(256)]
        public LangString Name { get; set; } = default!;

        public Guid DescriptionId { get; set; }

        [MinLength(1)]
        [MaxLength(256)]
        public LangString Description { get; set; } = default!;
        
        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; } = default!;

        public ICollection<Contract>? Contracts { get; set; }

        public ICollection<Course>? Courses { get; set; }
    }
}