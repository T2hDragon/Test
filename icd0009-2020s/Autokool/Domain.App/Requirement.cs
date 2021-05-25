using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain.App
{
    public class Requirement: DomainEntityId
    {
        public Guid NameId { get; set; }

        [MinLength(1)]
        [MaxLength(256)]
        public LangString Name { get; set; } = default!;

        public Guid DescriptionId { get; set; }

        [MinLength(1)]
        [MaxLength(256)]
        public LangString Description { get; set; } = default!;
        
        public double Price { get; set; }
        
        public int? Amount { get; set; } = null;



        public ICollection<CourseRequirement>? CourseRequirements { get; set; }
    }
}