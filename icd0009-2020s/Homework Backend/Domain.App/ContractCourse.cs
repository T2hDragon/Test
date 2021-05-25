using System;
using System.Collections.Generic;
using Domain.Base;

namespace Domain.App
{
    public class ContractCourse: DomainEntityId
    {

        public Guid? ContractId { get; set; }
        public Contract? Contract { get; set; }
        
        public Guid? CourseId { get; set; }
        public Course? Course { get; set; }

        public ICollection<Lesson>? Lessons { get; set; }
    }
}