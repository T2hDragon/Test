using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Title: DomainEntityId
    {
        [MinLength(1)]
        [MaxLength(256)]
        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Title), Name = nameof(Name))]
        public string Name { get; set; } = default!;

        [Display(ResourceType = typeof(Resources.BLL.App.DTO.Title), Name = nameof(Contracts))]
        public ICollection<Contract>? Contracts { get; set; }
    }

}