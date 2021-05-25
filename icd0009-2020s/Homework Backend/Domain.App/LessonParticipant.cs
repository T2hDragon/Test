using System;
using System.Collections.Generic;
using Domain.Base;

namespace Domain.App
{
    public class LessonParticipant: DomainEntityId
    {
        public double? Price { get; set; }

        public Guid? StudentId { get; set; }
        public Contract? Student { get; set; }

        public Guid? TeacherWorkHoursId { get; set; }
        public WorkHour? TeacherWorkHours { get; set; }

        public Guid? LessonId { get; set; }
        public Lesson? Lesson { get; set; }

        private ICollection<LessonParticipantConfirmation>? LessonParticipantConfirmations { get; set; }

        public ICollection<LessonParticipantNote>? LessonParticipantNotes { get; set; }
    }
}