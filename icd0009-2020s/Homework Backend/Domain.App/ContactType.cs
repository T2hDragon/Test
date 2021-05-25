using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain.App
{
    public class ContactType: DomainEntityId
    {
        
        [MinLength(1)]
        [MaxLength(256)]
        public string Type { get; set; } = default!;

        public ICollection<Contact>? Contacts { get; set; }
    }
}