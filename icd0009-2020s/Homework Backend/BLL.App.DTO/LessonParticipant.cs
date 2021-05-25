using System;
using System.Collections.Generic;
using Domain.Base;
using LessonParticipantConfirmation = BLL.App.DTO.LessonParticipantConfirmation;
using LessonParticipantNote = BLL.App.DTO.LessonParticipantNote;
using WorkHour = BLL.App.DTO.WorkHour;

namespace BLL.App.DTO
{
    public class LessonParticipant: DomainEntityId
    {
        public double? Price { get; set; }

        public Guid? StudentId { get; set; }
        public BLL.App.DTO.Contract? Student { get; set; }

        public Guid? TeacherWorkHoursId { get; set; }
        public WorkHour? TeacherWorkHours { get; set; }

        public Guid? LessonId { get; set; }
        public BLL.App.DTO.Lesson? Lesson { get; set; }

        private ICollection<LessonParticipantConfirmation>? LessonParticipantConfirmations { get; set; }

        public ICollection<LessonParticipantNote>? LessonParticipantNotes { get; set; }
    }
}