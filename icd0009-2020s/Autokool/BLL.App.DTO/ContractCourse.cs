using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;
using Course = BLL.App.DTO.Course;
using Lesson = BLL.App.DTO.Lesson;

namespace BLL.App.DTO
{
    public class ContractCourse: DomainEntityId
    {
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.ContractCourse), Name = nameof(HourlyPay))]
        public double HourlyPay { get; set; } = default!;

        public Guid StatusId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.ContractCourse), Name = nameof(Status))]
        public Status? Status { get; set; }
        
        
        public Guid? ContractId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.ContractCourse), Name = nameof(Contract))]
        public BLL.App.DTO.Contract? Contract { get; set; }
        
        public Guid? CourseId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.ContractCourse), Name = nameof(Course))]
        public Course? Course { get; set; }
        
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.ContractCourse), Name = nameof(LessonParticipants))]
        public ICollection<LessonParticipant>? LessonParticipants { get; set; }
    }
}