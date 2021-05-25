using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Requirement: DomainEntityId
    {
        [MinLength(1)]
        [MaxLength(256)]
        public string Description { get; set; } = default!;

        [MinLength(1)]
        [MaxLength(256)]
        public string Name { get; set; } = default!;

        public double? Price { get; set; }

        public Guid? SubRequirementId { get; set; }
        public Requirement? SubRequirement { get; set; }

        [InverseProperty(nameof(Requirement.SubRequirement))]
        public ICollection<Requirement>? SubRequirements { get; set; }
        
        public Guid? UpperRequirementId { get; set; }
        public Requirement? UpperRequirement { get; set; }

        [InverseProperty(nameof(Requirement.UpperRequirement))]
        public ICollection<Requirement>? UpperRequirements { get; set; }
        
        public ICollection<DAL.App.DTO.CourseRequirement>? CourseRequirements { get; set; }
    }
}