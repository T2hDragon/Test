using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.School.ViewModels.Teacher
{
    /// <summary>
    /// Teacher course add View model
    /// </summary>
    public class TeacherCourseAddViewModel
    {
        /// <summary>
        /// Contract Course
        /// </summary>
        public BLL.App.DTO.ContractCourse ContractCourse { get; set; } = default!;
        
        /// <summary>
        /// Name
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Course select list
        /// </summary>
        public SelectList? CourseSelectList { get; set; }
        

    }
}