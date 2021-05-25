using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.App.DTO
{
    public class WorkHour: DomainEntityId
    {
        [MinLength(1)]
        [MaxLength(256)]
        public string Day { get; set; } = default!;

        public DateTime Start { get; set; } = default!;

        public DateTime? End { get; set; }

        public Guid? TeacherContractId { get; set; }
        public BLL.App.DTO.Contract? TeacherContract { get; set; }

        public ICollection<BLL.App.DTO.LessonParticipant>? LessonParticipants { get; set; }
    }
}