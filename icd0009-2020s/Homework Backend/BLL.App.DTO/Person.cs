using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO.Identity;
using Contracts.Domain.Base;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Person: DomainEntityId, IDomainAppUserId, IDomainAppUser<AppUser>
    {
        [MinLength(2, ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "ErrorMessageMinLength")]
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Person), Name = nameof(FirstName))]
        [Required(ErrorMessage = "Give me the value!")]
        [MaxLength(64, ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "ErrorMessageMaxLength")] 
        public string FirstName { get; set; } = default!;

        [MinLength(2, ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "ErrorMessageMinLength")]
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Person), Name = nameof(LastName))]
        [Required(ErrorMessage = "Give me the value!")]
        [MaxLength(64, ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "ErrorMessageMaxLength")] 
        public string LastName { get; set; } = default!;
        
        public ICollection<Contact>? Contacts { get; set; }
        
        // was added in DAL
        public int? ContactCount { get; set; }
        
        // is added in BLL
        public int UniqueContactTypeCount { get; set; }
        
        // the owner of the current record
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        public string FullName => FirstName + " " + LastName;

    }
}