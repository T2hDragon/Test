using System;
using System.Collections.Generic;
using Contracts.Domain.Base;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class Contract: DomainEntityId, IDomainAppUserId, IDomainAppUser<AppUser>
    {

        public double HourlyPay { get; set; } = default!;

        public Guid? DrivingSchoolId { get; set; }
        public DrivingSchool? DrivingSchool { get; set; }

        public Guid? ContractorId { get; set; }
        public AppUser? Contractor { get; set; }
        
        public Guid? ContractTakerId { get; set; }
        public AppUser? ContractTaker { get; set; }
                
        public Guid? TitleId { get; set; }
        
        // the owner of the current record
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        public ICollection<WorkHour>? WorkHours { get; set; }

        public ICollection<LessonParticipant>? LessonParticipants { get; set; }
    }
}