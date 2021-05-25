using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Admin.ViewModels.DrivingSchool
{
    /// <summary>
    /// Driving school creation and edit view model
    /// </summary>
    public class DrivingSchoolCreateEditViewModel
    {
        /// <summary>
        /// Handling driving school
        /// </summary>
        public BLL.App.DTO.DrivingSchool DrivingSchool { get; set; } = default!;

        /// <summary>
        /// List of users
        /// </summary>
        public SelectList? AppUserSelectList { get; set; }
    }
}