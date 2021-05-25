using System;
using System.Collections.Generic;
using Domain.App;

namespace PublicAPI.DTO.v1.Entities
{
    public class Contract
    {
        public Guid ContractId { get; set; } = default!;
        
        public Guid? DrivingSchoolId { get; set; }
        public string DrivingSchoolName { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Status { get; set; } = default!;
        public string Name { get; set; } = default!;
    }
}