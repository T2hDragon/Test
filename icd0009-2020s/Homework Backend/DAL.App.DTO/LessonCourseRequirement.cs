using System;
using Domain.Base;

namespace DAL.App.DTO
{
    public class LessonCourseRequirement: DomainEntityId
    {

        public Guid? CourseRequirementId { get; set; }
        public DAL.App.DTO.CourseRequirement? CourseRequirement { get; set; }
        
        public Guid? LessonId { get; set; }
        public DAL.App.DTO.Lesson? Lesson { get; set; }

    }
}