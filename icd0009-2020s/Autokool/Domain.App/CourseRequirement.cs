using System;
using System.Collections.Generic;
using Domain.Base;

namespace Domain.App
{
    public class CourseRequirement: DomainEntityId
    {
        
        public double Price { get; set; }
        
        public Guid DescriptionId { get; set; }

        public LangString Description { get; set; } = default!;
        public int? Amount { get; set; } = null;


        public Guid? CourseId { get; set; }
        public Course? Course { get; set; }

        public Guid? RequirementId { get; set; }
        public Requirement? Requirement { get; set; }
        public ICollection<Lesson>? Lessons { get; set; }
    }
}