using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.App.DTO
{
    public class Teacher
    {
        public Guid ContractId { get; set; } = default!;
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Teacher), Name = nameof(Courses))]
        public IEnumerable<Course>? Courses { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Teacher), Name = nameof(CoursesNameRep))]
        public string CoursesNameRep { get; set; } = default!;
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Teacher), Name = nameof(ContractCourses))]
        public IEnumerable<ContractCourse>? ContractCourses { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Teacher), Name = nameof(Email))]
        public string Email { get; set; } = default!;
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Teacher), Name = nameof(Name))]
        public string Name { get; set; } = default!;
    }
}