using System;
using System.Collections.Generic;
using Domain.Base;
using LessonCourseRequirement = BLL.App.DTO.LessonCourseRequirement;
using Requirement = BLL.App.DTO.Requirement;

namespace BLL.App.DTO
{
    public class CourseRequirement: DomainEntityId
    {

        public Guid? CourseId { get; set; }
        public BLL.App.DTO.Course? Course { get; set; }

        public Guid? RequirementId { get; set; }
        public Requirement? Requirement { get; set; }

        public ICollection<LessonCourseRequirement>? LessonCourseRequirements { get; set; }
    }
}