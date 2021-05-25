using System;
using System.Collections.Generic;
using Domain.Base;

namespace Domain.App
{
    public class ContractCourse: DomainEntityId
    {
        public double HourlyPay { get; set; } = default!;

        public Guid StatusId { get; set; }
        public Status? Status { get; set; }
        
        public Guid? ContractId { get; set; }
        public Contract? Contract { get; set; }
        
        public Guid? CourseId { get; set; }
        public Course? Course { get; set; }

        public ICollection<LessonParticipant>? LessonParticipants { get; set; }
    }
}