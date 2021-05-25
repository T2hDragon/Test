using System;
using System.ComponentModel.DataAnnotations;
using Domain.App;

namespace PublicAPI.DTO.v1
{
    public class Contact
    {
        [MinLength(1)]
        [MaxLength(256)]
        public string Value { get; set; } = default!;
        
        public Guid ContactTypeId { get; set; }
        
        public Guid PersonId { get; set; }
    }
}