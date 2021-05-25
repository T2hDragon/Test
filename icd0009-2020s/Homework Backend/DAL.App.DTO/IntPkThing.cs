using System.ComponentModel.DataAnnotations;
using Contracts.Domain.Base;
using Domain.Base;

namespace DAL.App.DTO
{
    public class IntPkThing: DomainEntityId<int>
    {
        [MaxLength(32)] 
        public string Payload { get; set; } = default!;
    }
}