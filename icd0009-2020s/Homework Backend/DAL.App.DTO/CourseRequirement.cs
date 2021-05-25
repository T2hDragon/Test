using System;
using System.Collections.Generic;
using Domain.App;
using Domain.Base;

namespace DAL.App.DTO
{
    public class CourseRequirement: DomainEntityId
    {

        public Guid? CourseId { get; set; }
        public DAL.App.DTO.Course? Course { get; set; }

        public Guid? RequirementId { get; set; }
        public Requirement? Requirement { get; set; }

        public ICollection<LessonCourseRequirement>? LessonCourseRequirements { get; set; }
    }
}