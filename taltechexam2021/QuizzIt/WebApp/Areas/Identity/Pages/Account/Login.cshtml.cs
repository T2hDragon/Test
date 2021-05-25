using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Domain.App.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Resources.Base.Areas.Identity.Pages.Account;

namespace WebApp.Areas.Identity.Pages.Account
{
    /// <summary>
    /// Login Model Controller
    /// </summary>
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly ILogger<LoginModel> _logger;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        /// <summary>
        /// Login model controller creation
        /// </summary>
        /// <param name="signInManager"></param>
        /// <param name="logger"></param>
        /// <param name="userManager"></param>
        public LoginModel(SignInManager<AppUser> signInManager,
            ILogger<LoginModel> logger,
            UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        /// <summary>
        /// Input
        /// </summary>
        [BindProperty] public InputModel Input { get; set; } = default!;

        /// <summary>
        /// External logins
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; } = default!;

        /// <summary>
        /// Return url
        /// </summary>
        public string? ReturnUrl { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        [TempData] public string ErrorMessage { get; set; } = "";

        /// <summary>
        /// Get poage data
        /// </summary>
        /// <param name="returnUrl"></param>
        public async Task OnGetAsync(string? returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage)) ModelState.AddModelError(string.Empty, ErrorMessage);

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        /// <summary>
        /// Page data handling
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result =
                    await _signInManager.PasswordSignInAsync(Input.UserName, Input.Password, Input.RememberMe, false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }

                if (result.RequiresTwoFactor)
                    return RedirectToPage("./LoginWith2fa", new {ReturnUrl = returnUrl, Input.RememberMe});
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        /// <summary>
        /// Input model
        /// </summary>
        public class InputModel
        {
            /// <summary>
            /// Username
            /// </summary>
            [Required(ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "ErrorMessage_Required")]
            [Display(Name = nameof(UserName), ResourceType = typeof(Login))] 
            public string UserName { get; set; } = default!;

            /// <summary>
            /// Password
            /// </summary>
            [DataType(DataType.Password)]
            [Required(ErrorMessageResourceType = typeof(Resources.Base.Common), ErrorMessageResourceName = "ErrorMessage_Required")]
            [Display(Name = nameof(Password), ResourceType = typeof(Login))] 
            public string Password { get; set; } = default!;

            /// <summary>
            /// Remember me
            /// </summary>
            [Display(Name = nameof(RememberMe), ResourceType = typeof(Login))] 
            public bool RememberMe { get; set; }
        }
    }
}