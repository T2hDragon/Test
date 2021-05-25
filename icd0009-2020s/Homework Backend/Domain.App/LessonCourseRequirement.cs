using System;
using Domain.Base;

namespace Domain.App
{
    public class LessonCourseRequirement: DomainEntityId
    {

        public Guid? CourseRequirementId { get; set; }
        public CourseRequirement? CourseRequirement { get; set; }
        
        public Guid? LessonId { get; set; }
        public Lesson? Lesson { get; set; }

    }
}