using System;
using System.Collections.Generic;
using Domain.App;
using Domain.Base;

namespace DAL.App.DTO
{

    public class Question: DomainEntityId
    {
        public string Value { get; set; } = default!;
        public int Order { get; set; } = default!;

        public Guid? QuizId { get; set; }
        public DAL.App.DTO.Quiz? Quiz { get; set; }

        public ICollection<Answer>? Answers { get; set; }


    }
}