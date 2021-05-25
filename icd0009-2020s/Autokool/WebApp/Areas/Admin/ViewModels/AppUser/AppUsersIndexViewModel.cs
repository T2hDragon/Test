using System.Collections.Generic;

namespace WebApp.Areas.Admin.ViewModels.AppUser
{
    /// <summary>
    /// AppUsers main page data model
    /// </summary>
    public class AppUsersIndexViewModel
    {
        /// <summary>
        /// AppUsers
        /// </summary>
        public IEnumerable<AppUsersViewModel> AppUsers { get; set; } = default!;
        /// <summary>
        /// Search view model
        /// </summary>
        public AppUserSearchViewModel SearchViewModel { get; set; } = default!;
    }
}