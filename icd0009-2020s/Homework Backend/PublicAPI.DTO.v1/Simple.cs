using System;
using System.ComponentModel.DataAnnotations;

namespace PublicAPI.DTO.v1
{
    public class Simple
    {
        [MaxLength(32)] 
        public string Payload { get; set; } = default!;
        public Guid AppUserId { get; set; }
    }
}