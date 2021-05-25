using System;
using System.Collections.Generic;

namespace PublicAPI.DTO.v1.Entities
{
    public class StudentCourseReport
    {
        public Guid Id { get; set; } = default!;
        public string CourseName { get; set; } = default!;
        public DrivingLessonProgress? DrivingRequirementProgress { get; set; }
        public IEnumerable<RequirementProgress> CheckmarkProgress { get; set; } = default!;
    }
}