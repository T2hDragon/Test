using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Requirement: DomainEntityId
    {
        public Guid DescriptionId { get; set; }
        [MinLength(1)]
        [MaxLength(256)]
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Requirement), Name = nameof(Description))]
        public string Description { get; set; } = default!;
        
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Requirement), Name = nameof(Price))]
        public double Price { get; set; }
        
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Requirement), Name = nameof(Amount))]
        public int? Amount { get; set; } = null;

        public Guid NameId { get; set; }
        [MinLength(1)]
        [MaxLength(256)]
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Requirement), Name = nameof(Name))]
        public string Name { get; set; } = default!;


        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Requirement), Name = nameof(CourseRequirements))]
        public ICollection<CourseRequirement>? CourseRequirements { get; set; }
    }
}