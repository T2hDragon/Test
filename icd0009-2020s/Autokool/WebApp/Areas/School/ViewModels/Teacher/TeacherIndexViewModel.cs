using System;
using System.Collections.Generic;
using BLL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.School.ViewModels.Teacher
{
    /// <summary>
    /// Teacher Index View Model
    /// </summary>
    public class TeacherIndexViewModel
    {
        /// <summary>
        /// Teachers
        /// </summary>
        public IEnumerable<BLL.App.DTO.Teacher> Teachers{ get; set; }  = default!;
        /// <summary>
        /// Invite succeeded
        /// </summary>
        public bool? InviteSucceeded { get; set; }
    }
}