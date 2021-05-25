using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Contact: DomainEntityId
    {
        
        [MinLength(1)]
        [MaxLength(256)]
        public string Value { get; set; } = default!;
        
        public Guid ContactTypeId { get; set; }
        public ContactType? ContactType { get; set; } = default!;
        
        public Guid PersonId { get; set; }
        public Person? Person { get; set; } = default!;

    }


}