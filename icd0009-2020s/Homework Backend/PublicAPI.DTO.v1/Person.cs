using System;
using System.ComponentModel.DataAnnotations;

namespace PublicAPI.DTO.v1
{
    public class PersonAdd
    {
        [MaxLength(64)] 
        public string FirstName { get; set; } = default!;

        [MaxLength(64)] 
        public string LastName { get; set; } = default!;
    }

    public class Person
    {
        public Guid Id { get; set; }
        
        [MaxLength(64)] 
        public string FirstName { get; set; } = default!;

        [MaxLength(64)] 
        public string LastName { get; set; } = default!;

        public string FullName { get; set; } = default!;

        public int ContactCount { get; set; }
    }

}