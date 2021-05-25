using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.School.ViewModels.CourseRequirement
{
    /// <summary>
    /// Course requirement edit view model
    /// </summary>
    public class CourseRequirementEditViewModel
    {
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