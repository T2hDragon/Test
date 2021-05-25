using System.ComponentModel.DataAnnotations;

namespace WebApp.Areas.Admin.ViewModels.AppUser
{
    /// <summary>
    /// App user search view model
    /// </summary>
    public class AppUserSearchViewModel
    {

        /// <summary>
        /// Search value
        /// </summary>
        [MinLength(2)]
        [MaxLength(1024)]
        public string? search { get; set; }


        /// <summary>
        /// Does search include deactivated users
        /// </summary>
        public bool includeDeactivated { get; set; }
    }
}