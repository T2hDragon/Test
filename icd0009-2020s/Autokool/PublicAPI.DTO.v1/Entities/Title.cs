using System;
using System.ComponentModel.DataAnnotations;

namespace PublicAPI.DTO.v1.Entities
{
    public class Title
    {
        public Guid Id { get; set; }

        [MinLength(1)]
        [MaxLength(256)]
        public string Name { get; set; } = default!;
    }
}