using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{

    public class Status: DomainEntityId
    {
        [MinLength(1)]
        [MaxLength(64)]
        public string Name { get; set; } = default!;

        public ICollection<Contract>? Contracts { get; set; }
        public ICollection<ContractCourse>? ContractCourses { get; set; }

    }
}