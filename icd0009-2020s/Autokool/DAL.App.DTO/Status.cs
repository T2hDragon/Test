using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.App.DTO.Identity;
using Domain.App;
using Domain.Base;

namespace DAL.App.DTO
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