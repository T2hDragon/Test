using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Requirement: DomainEntityId
    {
        
        public Guid DescriptionId { get; set; }
        [MinLength(1)]
        [MaxLength(256)]
        public string Description { get; set; } = default!;
        
        public double Price { get; set; }
        
        public int? Amount { get; set; } = null;
        
        public Guid NameId { get; set; }
        [MinLength(1)]
        [MaxLength(256)]
        public string Name { get; set; } = default!;

        public ICollection<DAL.App.DTO.CourseRequirement>? CourseRequirements { get; set; }
    }
}