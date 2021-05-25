using System;
using System.Collections.Generic;
using Domain.App;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Lesson: DomainEntityId
    {

        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }

        public Guid? ContractCourseId { get; set; }
        public DAL.App.DTO.ContractCourse? ContractCourse { get; set; }

        public ICollection<LessonParticipant>? LessonParticipants { get; set; }
    }
}