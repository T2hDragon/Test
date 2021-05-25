using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace BLL.App.DTO
{
    public class PeriodReport
    {
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.PeriodReport), Name = nameof(Start))]
        public DateTime Start { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.PeriodReport), Name = nameof(End))]
        public DateTime End { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.PeriodReport), Name = nameof(DrivingLessons))]
        public IEnumerable<DrivingLesson> DrivingLessons { get; set; } = default!;
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.PeriodReport), Name = nameof(TotalSalary))]
        public double TotalSalary { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.PeriodReport), Name = nameof(TotalHours))]
        public double TotalHours { get; set; }
    }
}