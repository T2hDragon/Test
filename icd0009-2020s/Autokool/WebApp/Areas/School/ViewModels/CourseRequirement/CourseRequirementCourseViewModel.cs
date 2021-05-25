using System.Collections.Generic;

namespace WebApp.Areas.School.ViewModels.CourseRequirement
{
    /// <summary>
    /// Course Requirement Course View Model
    /// </summary>
    public class CourseRequirementCourseViewModel
    {
        /// <summary>
        /// Course Requirement
        /// </summary>
        public IEnumerable<BLL.App.DTO.CourseRequirement> CourseRequirements { get; set; } = default!;

        /// <summary>
        /// Course
        /// </summary>
        public BLL.App.DTO.Course Course { get; set; } = default!;
        
    }
}