using System;
using System.Collections.Generic;
using Domain.Base;

namespace BLL.App.DTO
{

    public class Question: DomainEntityId
    {
        public string Value { get; set; } = default!;
        public int Order { get; set; } = default!;

        public Guid? QuizId { get; set; }
        public Quiz? Quiz { get; set; }

        public ICollection<BLL.App.DTO.Answer>? Answers { get; set; } = default!;


    }
}