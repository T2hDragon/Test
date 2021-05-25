using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class Course: DomainEntityId
    {

        [MinLength(1)]
        [MaxLength(256)]
        public string Name { get; set; } = default!;

        [MinLength(1)]
        [MaxLength(256)]
        public string? Description { get; set; }

        public double Price { get; set; } = default!;

        [MinLength(1)]
        [MaxLength(256)]
        public string Category { get; set; } = default!;

        public Guid? DrivingSchoolId { get; set; }
        public DrivingSchool? DrivingSchool { get; set; }

        public ICollection<ContractCourse>? ContractCourses { get; set; }

        public ICollection<CourseRequirement>? CourseRequirements { get; set; }
        
    }
}