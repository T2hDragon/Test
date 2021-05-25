using System;

namespace WebApp.Areas.School.ViewModels.Teacher
{
    /// <summary>
    /// Test View Model
    /// </summary>
    public class TeacherViewModel
    {
        /// <summary>
        /// Time
        /// </summary>
        public DateTime Time { get; set; } = default;
        /// <summary>
        /// Month shift
        /// </summary>
        public int MonthShift { get; set; } = default!;
        /// <summary>
        /// Teacher
        /// </summary>
        public BLL.App.DTO.Teacher Teacher { get; set; } = default!;
        /// <summary>
        /// Period Report
        /// </summary>
        public BLL.App.DTO.PeriodReport PeriodReport { get; set; } = default!;
    }
}