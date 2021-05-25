using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace DAL.App.DTO
{
    public class ContactType: DomainEntityId
    {
        
        [MinLength(1)]
        [MaxLength(256)]
        [Display(ResourceType = typeof(Resources.DAL.App.DTO.ContactType), Name = "ContactTypeValue")]
        public string Type { get; set; } = default!;

        public ICollection<Contact>? Contacts { get; set; }

        public int? ContactCount { get; set; }
    }
}