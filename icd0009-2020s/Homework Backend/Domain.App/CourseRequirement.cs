using System;
using System.Collections.Generic;
using Domain.Base;

namespace Domain.App
{
    public class CourseRequirement: DomainEntityId
    {

        public Guid? CourseId { get; set; }
        public Course? Course { get; set; }

        public Guid? RequirementId { get; set; }
        public Requirement? Requirement { get; set; }

        public ICollection<LessonCourseRequirement>? LessonCourseRequirements { get; set; }
    }
}