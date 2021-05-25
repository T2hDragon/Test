using System;
using Microsoft.Data.SqlClient.Server;

namespace PublicAPI.DTO.v1.Entities
{
    public class StudentCourse
    {
        public Guid Id { get; set; } = default!;
        
        public string Name { get; set; } = default!;
        
        public string Description { get; set; } = default!;
        
        public string Category { get; set; } = default!;
        public Guid ContractId  { get; set; } = default!;
        public Guid CourseId  { get; set; } = default!;
    }
}