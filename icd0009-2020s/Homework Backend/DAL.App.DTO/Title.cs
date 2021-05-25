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
        public Guid Name { get; set; }

        public ICollection<Contract>? Contracts { get; set; }
    }

}