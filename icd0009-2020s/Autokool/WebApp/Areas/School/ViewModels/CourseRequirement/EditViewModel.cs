using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.School.ViewModels.CourseRequirement
{
    /// <summary>
    /// Edit view model
    /// </summary>
    public class EditViewModel
    {
        /// <summary>
        /// All course Requirements
        /// </summary>
        public IEnumerable<BLL.App.DTO.CourseRequirement> AllCourseRequirements { get; set; } = default!;

        /// <summary>
        /// Course Requirement
        /// </summary>
        public BLL.App.DTO.CourseRequirement CourseRequirement { get; set; } = default!;

        /// <summary>
        /// Requirements
        /// </summary>
        public SelectList? RequirementSelectList { get; set; }
        
        /// <summary>
        /// Course
        /// </summary>
        public BLL.App.DTO.Course Course { get; set; } = default!;

    }
}