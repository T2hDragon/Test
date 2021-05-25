using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Admin.ViewModels.AppUser
{
    /// <summary>
    ///     View model for AppUser Creation and Editing
    /// </summary>
    public class AppUserCreateManageViewModel
    {
        /// <summary>
        /// Username
        /// </summary>
        [MinLength(4)]
        [MaxLength(256)] 
        public string UserName { get; set; } = default!;
        
        /// <summary>
        /// Firstname
        /// </summary>
        [MinLength(4)]
        [MaxLength(256)]
        public string Firstname { get; set; } = default!;
        
        /// <summary>
        /// Lastname
        /// </summary>
        [MinLength(4)]
        [MaxLength(256)]
        public string Lastname { get; set; } = default!;

        /// <summary>
        /// Email
        /// </summary>
        [EmailAddress]
        [MaxLength(256)]
        public string Email { get; set; } = default!;

        /// <summary>
        /// Password
        /// </summary>
        [DataType("password")]
        [MinLength(8)]
        [MaxLength(256)]
        public string Password { get; set; } = default!;

        /// <summary>
        /// Has email been confirmed
        /// </summary>
        public bool IsEmailConfirmed { get; set; }

        /// <summary>
        /// Given roles
        /// </summary>
        public IEnumerable<string> SelectedRoles { get; set; } = default!;

        /// <summary>
        /// All possible roles
        /// </summary>
        public MultiSelectList? Roles { get; set; }
    }
}