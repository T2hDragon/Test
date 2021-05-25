using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Title: DomainEntityId
    {
        [MinLength(1)]
        [MaxLength(256)]
        public string Name { get; set; } = default!;

        public ICollection<Contract>? Contracts { get; set; }
        
    }

}