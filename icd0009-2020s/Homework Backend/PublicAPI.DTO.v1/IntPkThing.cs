using System.ComponentModel.DataAnnotations;

namespace PublicAPI.DTO.v1
{
    public class IntPkThingAdd
    {
        [MaxLength(32)] 
        public string Payload { get; set; } = default!;
    }
    
    public class IntPkThing
    {
        public int Id { get; set; }
        
        [MaxLength(32)] 
        public string Payload { get; set; } = default!;
    }
}