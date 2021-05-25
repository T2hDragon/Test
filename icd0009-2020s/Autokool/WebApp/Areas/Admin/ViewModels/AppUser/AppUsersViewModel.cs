using System.ComponentModel.DataAnnotations;

namespace WebApp.Areas.Admin.ViewModels.AppUser
{
    /// <summary>
    /// App users View Model
    /// </summary>
    public class AppUsersViewModel
    {
        /// <summary>
        /// UserName
        /// </summary>
        [MinLength(4)]
        [MaxLength(64)]

        public string UserName { get; set; } = default!;
        /// <summary>
        /// Firstname
        /// </summary>
        [MinLength(4)]
        [MaxLength(256)]
        public string Firstname { get; set; } = default!;
        
        /// <summary>
        /// LastName
        /// </summary>
        [MinLength(4)]
        [MaxLength(256)]
        public string Lastname { get; set; } = default!;

        /// <summary>
        /// Email
        /// </summary>
        [EmailAddress]
        public string Email { get; set; } = default!;

        /// <summary>
        /// Roles
        /// </summary>
        public string Roles { get; set; } = default!;


        /// <summary>
        /// Is account locked down
        /// </summary>
        public bool IsLocked { get; set; }
    }
}