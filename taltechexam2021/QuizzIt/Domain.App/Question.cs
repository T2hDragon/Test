using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{

    public class Question: DomainEntityId
    {
        public string Value { get; set; } = default!;
        public int Order { get; set; } = default!;

        public Guid? QuizId { get; set; } 
        public Quiz? Quiz { get; set; }

        public ICollection<Answer>? Answers { get; set; }


    }
}