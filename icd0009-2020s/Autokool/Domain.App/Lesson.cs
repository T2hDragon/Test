using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class Lesson: DomainEntityId
    {
        [DataType(DataType.DateTime)]
        public DateTime? Start { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? End { get; set; }

        public ICollection<LessonParticipant>? LessonParticipants { get; set; }

        public Guid CourseRequirementId { get; set; } = default!;
        public CourseRequirement CourseRequirement { get; set; } = default!;
    }
}