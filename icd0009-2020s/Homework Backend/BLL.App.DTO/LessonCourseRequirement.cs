using System;
using Domain.Base;

namespace BLL.App.DTO
{
    public class LessonCourseRequirement: DomainEntityId
    {

        public Guid? CourseRequirementId { get; set; }
        public BLL.App.DTO.CourseRequirement? CourseRequirement { get; set; }
        
        public Guid? LessonId { get; set; }
        public BLL.App.DTO.Lesson? Lesson { get; set; }

    }
}