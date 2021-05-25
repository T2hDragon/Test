using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO.Identity;
using Domain.Base;


namespace BLL.App.DTO
{
    public class Contract: DomainEntityId
    {
        public Guid? DrivingSchoolId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Contract), Name = nameof(DrivingSchool))]
        public DrivingSchool? DrivingSchool { get; set; }

        public Guid TitleId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Contract), Name = nameof(Title))]
        public Title? Title { get; set; }
        
                
        public Guid StatusId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Contract), Name = nameof(Status))]
        public Status? Status { get; set; }
        
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Contract), Name = nameof(ContractCourses))]
        public ICollection<ContractCourse>? ContractCourses { get; set; }

        // Owner
        public Guid AppUserId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Contract), Name = nameof(ContractCourses))]
        public AppUser? AppUser { get; set; } = default!;
    }
}