using System;
using System.Collections.Generic;
using Contracts.Domain.Base;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class Contract: DomainEntityId, IDomainAppUserId, IDomainAppUser<AppUser>
    {
        
        public Guid? DrivingSchoolId { get; set; }
        public DrivingSchool? DrivingSchool { get; set; }

        public Guid TitleId { get; set; }
        public Title? Title { get; set; }
        
                
        public Guid StatusId { get; set; }
        public Status? Status { get; set; }

        public ICollection<ContractCourse>? ContractCourses { get; set; }


        // Owner
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; } = default!;
    }
}