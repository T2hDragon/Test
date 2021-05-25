using System;

namespace PublicAPI.DTO.v1.Entities
{
    public class RequirementProgress
    {
        public Guid Id { get; set; } = default!;
        public string RequirementName { get; set; } =  default!;
        public bool IsCompleted { get; set; }= default!;

    }
}