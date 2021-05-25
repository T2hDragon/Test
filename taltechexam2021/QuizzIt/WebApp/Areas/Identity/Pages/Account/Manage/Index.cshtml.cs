using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DAL.App.EF;
using Domain.App.Identity;
using Resources.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Index = Resources.Base.Areas.Identity.Pages.Account.Manage.Index;

namespace WebApp.Areas.Identity.Pages.Account.Manage
{
    /// <summary>
    /// Index model controller
    /// </summary>
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDbContext _ctx;

        /// <summary>
        /// Index model controller constructor
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="ctx"></param>
        public IndexModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, 
            AppDbContext ctx)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _ctx = ctx;
        }

        /// <summary>
        /// Username
        /// </summary>
        [Display(Name = nameof(Username), ResourceType = typeof(Index))]
        public string? Username { get; set; }


        /// <summary>
        /// Status message
        /// </summary>
        [TempData] public string StatusMessage { get; set; } = default!;

        /// <summary>
        /// Input
        /// </summary>
        [BindProperty] public InputModel Input { get; set; } = default!;

        /// <summary>
        /// Input model
        /// </summary>
        public class InputModel
        {
            /// <summary>
            /// Phone number
            /// </summary>
            [Phone(ErrorMessageResourceName = "ErrorMessage_NotValidPhone", ErrorMessageResourceType = typeof(Common))]
            [Display(Name = nameof(PhoneNumber), ResourceType = typeof(Index))]
            public string? PhoneNumber { get; set; }


            /// <summary>
            /// First name
            /// </summary>
            [StringLength(128, ErrorMessageResourceName = "ErrorMessage_StringLengthMinMax", ErrorMessageResourceType = typeof(Common), MinimumLength = 1)]
            [Display(Name = nameof(FirstName), ResourceType = typeof(Index))]
            public string FirstName { get; set; } = default!;
            
            /// <summary>
            /// Last name
            /// </summary>
            [StringLength(128, ErrorMessageResourceName = "ErrorMessage_StringLengthMinMax", ErrorMessageResourceType = typeof(Common), MinimumLength = 1)]
            [Display(Name = nameof(LastName), ResourceType = typeof(Index))]
            public string LastName { get; set; } = default!;
            
        }

        private async Task LoadAsync(AppUser user)
        {
            
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                FirstName = user.Firstname ?? "",
                LastName = user.Lastname ?? "",
            };
        }

        /// <summary>
        /// Get page init data
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(string.Format(Index.Unable_to_load_user_with_ID,_userManager.GetUserId(User)));
            }

            await LoadAsync(user);
            return Page();
        }

        /// <summary>
        /// Handle user input
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(string.Format(Index.Unable_to_load_user_with_ID,_userManager.GetUserId(User)));
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = Index.Unexpected_error_when_trying_to_set_phone_number;
                    return RedirectToPage();
                }
            }


            user.Firstname = Input.FirstName;
            user.Lastname = Input.LastName;

          
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                StatusMessage = Index.Unexpected_error_when_trying_to_update_profile_data;
                return RedirectToPage();
            }
            
            
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = Index.Your_profile_has_been_updated;

            return RedirectToPage();
        }
    }
}