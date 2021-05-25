using System;

namespace PublicAPI.DTO.v1.Entities
{
    public class DrivingLesson
    {
        public Guid Id { get; set; } = default!;
        public string Teachers  { get; set; } = default!;
        public string Students { get; set; } = default!;
        public string CourseName { get; set; } = default!;
        public DateTime Start { get; set; } = default!;
        public DateTime End { get; set; } = default!;
    }
}