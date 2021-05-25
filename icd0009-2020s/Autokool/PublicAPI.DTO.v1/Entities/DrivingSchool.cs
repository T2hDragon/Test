using System;

namespace PublicAPI.DTO.v1.Entities
{
    public class DrivingSchool
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        
    }
}