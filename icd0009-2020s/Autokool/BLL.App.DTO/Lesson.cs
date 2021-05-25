using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;
using LessonParticipant = BLL.App.DTO.LessonParticipant;

namespace BLL.App.DTO
{
    public class Lesson: DomainEntityId
    {
        [DataType(DataType.DateTime)]
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Lesson), Name = nameof(Start))]
        public DateTime? Start { get; set; }
        [DataType(DataType.DateTime)]
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Lesson), Name = nameof(End))]
        public DateTime? End { get; set; }

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Lesson), Name = nameof(LessonParticipants))]
        public ICollection<LessonParticipant>? LessonParticipants { get; set; }
        
        public Guid CourseRequirementId { get; set; } = default!;
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Lesson), Name = nameof(CourseRequirement))]
        public CourseRequirement CourseRequirement { get; set; } = default!;    
    }
}