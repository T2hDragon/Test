using System;
using System.Collections.Generic;
using Domain.App;
using Domain.Base;

namespace DAL.App.DTO
{
    public class LessonParticipant: DomainEntityId
    {
        public double? Price { get; set; }

        public Guid? StudentId { get; set; }
        public DAL.App.DTO.Contract? Student { get; set; }

        public Guid? TeacherWorkHoursId { get; set; }
        public WorkHour? TeacherWorkHours { get; set; }

        public Guid? LessonId { get; set; }
        public DAL.App.DTO.Lesson? Lesson { get; set; }

        private ICollection<LessonParticipantConfirmation>? LessonParticipantConfirmations { get; set; }

        public ICollection<LessonParticipantNote>? LessonParticipantNotes { get; set; }
    }
}