using System;
using System.ComponentModel.DataAnnotations;

namespace PublicAPI.DTO.v1.Entities
{
    public class Requirement
    {
        public Guid Id { get; set; }
        [MinLength(1)]
        [MaxLength(256)]
        public string Description { get; set; } = default!;
        
        public double Price { get; set; }
        
        public int? Amount { get; set; } = null;

        [MinLength(1)]
        [MaxLength(256)]
        public string Name { get; set; } = default!;
    }
}