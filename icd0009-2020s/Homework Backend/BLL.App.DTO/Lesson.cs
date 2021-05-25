using System;
using System.Collections.Generic;
using Domain.Base;
using LessonParticipant = BLL.App.DTO.LessonParticipant;

namespace BLL.App.DTO
{
    public class Lesson: DomainEntityId
    {

        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }

        public Guid? ContractCourseId { get; set; }
        public BLL.App.DTO.ContractCourse? ContractCourse { get; set; }

        public ICollection<LessonParticipant>? LessonParticipants { get; set; }
    }
}