using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.App.DTO
{
    public class CourseRequirement: DomainEntityId
    {
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.CourseRequirement), Name = nameof(Price))]
        public double Price { get; set; }
        
        public Guid DescriptionId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.CourseRequirement), Name = nameof(Description))]
        public string Description { get; set; } = default!;
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.CourseRequirement), Name = nameof(Amount))]
        public int? Amount { get; set; } = null;


        public Guid? CourseId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.CourseRequirement), Name = nameof(Course))]
        public Course? Course { get; set; }

        public Guid? RequirementId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.CourseRequirement), Name = nameof(Requirement))]
        public Requirement? Requirement { get; set; }

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.CourseRequirement), Name = nameof(Lessons))]
        public ICollection<Lesson>? Lessons { get; set; }
    }
}