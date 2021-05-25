using System;
using System.Collections.Generic;
using Domain.Base;
using Course = BLL.App.DTO.Course;
using Lesson = BLL.App.DTO.Lesson;

namespace BLL.App.DTO
{
    public class ContractCourse: DomainEntityId
    {

        public Guid? ContractId { get; set; }
        public BLL.App.DTO.Contract? Contract { get; set; }
        
        public Guid? CourseId { get; set; }
        public Course? Course { get; set; }

        public ICollection<Lesson>? Lessons { get; set; }
    }
}