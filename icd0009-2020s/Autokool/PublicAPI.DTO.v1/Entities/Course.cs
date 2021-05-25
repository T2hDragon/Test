using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicAPI.DTO.v1.Entities
{
    public class Course
    {
        public Guid Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public double Price { get; set; } = default!;
        public string Category { get; set; } = default!;

    }
}