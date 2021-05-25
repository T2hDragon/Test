using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{

    public class Answer: DomainEntityId
    {
        public string Value { get; set; } = default!;
        public bool IsCorrect { get; set; } = default!;
        public int Order { get; set; } = default!;

        public Guid? QuestionId { get; set; }
        public Question? Question { get; set; }
        
        public ICollection<PickedAnswer>? PickedAnswers { get; set; }


    }
}