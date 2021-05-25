using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using Contracts.Domain.Base;
using DAL.App.DTO.Identity;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Person: DomainEntityId, IDomainAppUserId, IDomainAppUser<AppUser>
    {
        [MaxLength(64)] public string FirstName { get; set; } = default!;

        [MaxLength(64)] public string LastName { get; set; } = default!;
        
        public ICollection<Contact>? Contacts { get; set; }
        
        public int? ContactCount { get; set; }

                
        [InverseProperty(nameof(Contract.Contractor))]
        public ICollection<Contract>? ContractorContracts { get; set; }
        
        [InverseProperty(nameof(Contract.ContractTaker))]
        public ICollection<Contract>? Contracts { get; set; }

        // the owner of the current record
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        public string FullName => FirstName + " " + LastName;

    }
}