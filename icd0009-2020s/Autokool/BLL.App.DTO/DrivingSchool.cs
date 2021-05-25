using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.App.DTO
{
    public class DrivingSchool: DomainEntityId
    {
        public Guid NameId { get; set; }

        [MinLength(1)]
        [MaxLength(256)]
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.DrivingSchool), Name = nameof(Name))]
        public string Name { get; set; } = default!;

        public Guid DescriptionId { get; set; }
        [MinLength(1)]
        [MaxLength(256)]
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.DrivingSchool), Name = nameof(Description))]
        public string Description { get; set; } = default!;
        
        public Guid AppUserId { get; set; } = default!;
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.DrivingSchool), Name = nameof(AppUser))]
        public BLL.App.DTO.Identity.AppUser? AppUser { get; set; } = default!;

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.DrivingSchool), Name = nameof(Contracts))]
        public ICollection<Contract>? Contracts { get; set; }

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.DrivingSchool), Name = nameof(Courses))]
        public ICollection<Course>? Courses { get; set; }   
    }
}