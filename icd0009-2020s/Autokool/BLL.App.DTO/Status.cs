using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Status: DomainEntityId
    {
        [MinLength(1)]
        [MaxLength(64)]
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Status), Name = nameof(Name))]
        public string Name { get; set; } = default!;

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Status), Name = nameof(Contracts))]
        public ICollection<Contract>? Contracts { get; set; }
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Status), Name = nameof(ContractCourses))]
        public ICollection<ContractCourse>? ContractCourses { get; set; }

    }
}