using System;
using System.Collections.Generic;
using DAL.App.DTO.Identity;
using Domain.App;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Contract: DomainEntityId
    {

        public Guid? DrivingSchoolId { get; set; }
        public DrivingSchool? DrivingSchool { get; set; }

        public Guid TitleId { get; set; }
        public Title? Title { get; set; }
        
        public Guid StatusId { get; set; }
        public Status Status { get; set; } = default!;
        
        public ICollection<ContractCourse>? ContractCourses { get; set; }

        // Owner
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; } = default!;
    }
}