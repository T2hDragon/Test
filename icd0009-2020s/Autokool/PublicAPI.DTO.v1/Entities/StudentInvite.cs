using System;

namespace PublicAPI.DTO.v1.Entities
{
    public class StudentInvite
    {
        public string Username { get; set; } = default!;
        public Guid SchoolId { get; set; } = default!;

    }
}