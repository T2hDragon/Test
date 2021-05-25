using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class IntPkThing: DomainEntityId<int> 
    {
        [MaxLength(32)] 
        public string Payload { get; set; } = default!;
    }
}