using System;
using System.Collections.Generic;
using Domain.Base;
using DrivingSchool = BLL.App.DTO.DrivingSchool;
using LessonParticipant = BLL.App.DTO.LessonParticipant;
using WorkHour = BLL.App.DTO.WorkHour;

namespace BLL.App.DTO
{
    public class Contract: DomainEntityId
    {

        public double HourlyPay { get; set; } = default!;

        public Guid? DrivingSchoolId { get; set; }
        public DrivingSchool? DrivingSchool { get; set; }

        public Guid? ContractorId { get; set; }
        public Domain.App.Person? Contractor { get; set; }
        
        public Guid? ContractTakerId { get; set; }
        public Domain.App.Person? ContractTaker { get; set; }
                
        public Guid? TitleId { get; set; }

        public ICollection<WorkHour>? WorkHours { get; set; }

        public ICollection<LessonParticipant>? LessonParticipants { get; set; }
    }
}