using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.App.DTO
{
    public class DrivingLesson
    {
        public Guid LessonId { get; set; } = default!;
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.DrivingLesson), Name = nameof(Teachers))]
        public string Teachers { get; set; } = default!;
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.DrivingLesson), Name = nameof(Students))]
        public string Students { get; set; } = default!;
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.DrivingLesson), Name = nameof(CourseName))]
        public string CourseName { get; set; } = default!;
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.DrivingLesson), Name = nameof(Start))]
        public DateTime Start { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.DrivingLesson), Name = nameof(End))]
        public DateTime End { get; set; }
    }
}