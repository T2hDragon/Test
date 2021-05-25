using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;
using CourseRequirement = BLL.App.DTO.CourseRequirement;
using DrivingSchool = BLL.App.DTO.DrivingSchool;

namespace BLL.App.DTO
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

        public ICollection<BLL.App.DTO.ContractCourse>? ContractCourses { get; set; }

        public ICollection<CourseRequirement>? CourseRequirements { get; set; }
        
    }
}