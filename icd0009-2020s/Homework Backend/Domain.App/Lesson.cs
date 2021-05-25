using System;
using System.Collections.Generic;
using Domain.Base;

namespace Domain.App
{
    public class Lesson: DomainEntityId
    {

        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }

        public Guid? ContractCourseId { get; set; }
        public ContractCourse? ContractCourse { get; set; }

        public ICollection<LessonParticipant>? LessonParticipants { get; set; }
    }
}