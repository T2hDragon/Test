using System;

namespace WebApp.Areas.Admin.ViewModels.AppUser
{
    /// <summary>
    /// View model for App user details
    /// </summary>
    public class AppUserDetailsViewModel
    {
        /// <summary>
        /// Creation DateTime
        /// </summary>
        public DateTime CreatedAt { get; set; }
        
        /// <summary>
        ///  Username
        /// </summary>
        public string UserName { get; set; } = default!;
        
        /// <summary>
        /// Firstname
        /// </summary>
        public string FirstName { get; set; } = default!;
        
        /// <summary>
        /// Lastname
        /// </summary>
        public string LastName { get; set; } = default!;
        
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; } = default!;
        
        /// <summary>
        /// Has email been confirmed
        /// </summary>
        public bool EmailConfirmed { get; set; }
        
        /// <summary>
        /// Phone number
        /// </summary>
        public string PhoneNumber { get; set; } = default!;
        
        /// <summary>
        /// Has phone number been confirmed
        /// </summary>
        public bool PhoneNumberConfirmed { get; set; }
        
        /// <summary>
        /// Is user locked
        /// </summary>
        public bool LockoutEnabled { get; set; }
    }
}