using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain.Base;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class Simple : DomainEntityId, IDomainAppUserId, IDomainAppUser<AppUser>
    {
        [MaxLength(32)] public string Payload { get; set; } = default!;
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}