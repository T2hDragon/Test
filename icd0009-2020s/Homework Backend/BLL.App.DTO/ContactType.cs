using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.App.DTO
{
    public class ContactType: DomainEntityId
    {
        
        [MinLength(1)]
        [MaxLength(256)]
        public string Type { get; set; } = default!;

        public ICollection<Contact>? Contacts { get; set; }
    }
}