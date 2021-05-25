using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.App.DTO
{
    public class LessonParticipant: DomainEntityId
    {
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.LessonParticipant), Name = nameof(Price))]
        public double? Price { get; set; }

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.LessonParticipant), Name = nameof(OccurrenceConfirmation))]
        public bool? OccurrenceConfirmation { get; set; }
        
        public Guid? LessonNoteId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.LessonParticipant), Name = nameof(LessonNote))]
        public string? LessonNote { get; set; }
                
        [DataType(DataType.DateTime)]
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.LessonParticipant), Name = nameof(Start))]
        public DateTime Start { get; set; } = default!;
        [DataType(DataType.DateTime)]
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.LessonParticipant), Name = nameof(End))]
        public DateTime? End { get; set; }
        
        public Guid? ContractCourseId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.LessonParticipant), Name = nameof(ContractCourse))]
        public ContractCourse? ContractCourse { get; set; }

        public Guid? LessonId { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.LessonParticipant), Name = nameof(Lesson))]
        public Lesson? Lesson { get; set; }
    }
}