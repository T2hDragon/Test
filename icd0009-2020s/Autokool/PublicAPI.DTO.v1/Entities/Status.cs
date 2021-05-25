using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO;

namespace PublicAPI.DTO.v1.Entities
{
    public class Status
    {
        public Guid Id { get; set; }

        [MinLength(1)]
        [MaxLength(64)]
        public string Name { get; set; } = default!;
        
    }
}