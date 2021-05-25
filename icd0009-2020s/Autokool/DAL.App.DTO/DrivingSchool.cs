using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace DAL.App.DTO
{
    public class DrivingSchool: DomainEntityId
    {
        public Guid NameId { get; set; }
        [MinLength(1)]
        [MaxLength(256)]
        public string Name { get; set; } = default!;

        public Guid DescriptionId { get; set; }
        [MinLength(1)]
        [MaxLength(256)]
        public string Description { get; set; } = default!;
        
        public Guid AppUserId { get; set; } = default!;
        public DAL.App.DTO.Identity.AppUser? AppUser { get; set; } = default!;

        public ICollection<DAL.App.DTO.Contract>? Contracts { get; set; }

        public ICollection<DAL.App.DTO.Course>? Courses { get; set; }
    }
}