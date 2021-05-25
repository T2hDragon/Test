using System;

namespace PublicAPI.DTO.v1.Entities
{
    public class Student
    {
        public Guid Id { get; set; } = default!;
        public string Username { get; set; } =  default!;
        public string FullName { get; set; }= default!;
        public string Email { get; set; } = default!;

    }
}