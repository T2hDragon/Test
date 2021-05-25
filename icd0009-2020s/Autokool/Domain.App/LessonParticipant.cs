using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class LessonParticipant: DomainEntityId
    {
        public double? Price { get; set; }

        public bool? OccurrenceConfirmation { get; set; }
        
        public Guid? LessonNoteId { get; set; }
        public LangString? LessonNote { get; set; }
                
        [DataType(DataType.DateTime)]
        public DateTime Start { get; set; } = default!;
        [DataType(DataType.DateTime)]
        public DateTime? End { get; set; }
        
        public Guid? ContractCourseId { get; set; }
        public ContractCourse? ContractCourse { get; set; }

        public Guid? LessonId { get; set; }
        public Lesson? Lesson { get; set; }
    }
}