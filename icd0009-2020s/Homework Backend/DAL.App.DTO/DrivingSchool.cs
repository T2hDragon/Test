using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace DAL.App.DTO
{
    public class DrivingSchool: DomainEntityId
    {

        [MinLength(1)]
        [MaxLength(256)]
        public string Name { get; set; } = default!;

        [MinLength(1)]
        [MaxLength(256)]
        public string? Description { get; set; }

        public ICollection<DAL.App.DTO.Contract>? Contracts { get; set; }

        public ICollection<DAL.App.DTO.Course>? Courses { get; set; }
    }
}