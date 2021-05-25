using System;
using System.Collections.Generic;
using Domain.App;
using Domain.Base;

namespace DAL.App.DTO
{
    public class ContractCourse: DomainEntityId
    {

        public Guid? ContractId { get; set; }
        public DAL.App.DTO.Contract? Contract { get; set; }
        
        public Guid? CourseId { get; set; }
        public Course? Course { get; set; }

        public ICollection<Lesson>? Lessons { get; set; }
    }
}