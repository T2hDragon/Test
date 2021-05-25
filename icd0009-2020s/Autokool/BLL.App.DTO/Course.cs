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
        public Guid NameId { get; set; }
        [MinLength(1)]
        [MaxLength(256)]
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Course), Name = nameof(Name))]
        public string Name { get; set; } = default!;

        public Guid DescriptionId { get; set; }
        [MinLength(1)]
        [MaxLength(256)]
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Course), Name = nameof(Description))]
        public string Description { get; set; } = default!;

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Course), Name = nameof(Price))]
        public double Price { get; set; } = default!;

        [MinLength(1)]
        [MaxLength(256)]
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Course), Name = nameof(Category))]
        public string Category { get; set; } = default!;

        public Guid? DrivingSchoolId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Course), Name = nameof(DrivingSchool))]
        public DrivingSchool? DrivingSchool { get; set; }

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Course), Name = nameof(ContractCourses))]
        public ICollection<BLL.App.DTO.ContractCourse>? ContractCourses { get; set; }

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Course), Name = nameof(CourseRequirements))]
        public ICollection<CourseRequirement>? CourseRequirements { get; set; }
        
    }
}