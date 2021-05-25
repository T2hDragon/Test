using System;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO.Identity;
using Contracts.Domain.Base;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Simple : DomainEntityId, IDomainAppUserId, IDomainAppUser<AppUser>
    {
        [MaxLength(32)] public string Payload { get; set; } = default!;
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}