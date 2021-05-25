using System;

namespace PublicAPI.DTO.v1.Helper
{
    public class DrivingLessonCreation
    {
        public Guid TeacherId  { get; set; } = default!;
        public Guid StudentCourseId  { get; set; } = default!;
        public DateTime Start { get; set; } = default!;
        public double Length { get; set; } = default!;
    }
}