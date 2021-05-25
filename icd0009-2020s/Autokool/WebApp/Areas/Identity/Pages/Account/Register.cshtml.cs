using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Domain.App.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Resources.Base.Areas.Identity.Pages.Account;

namespace WebApp.Areas.Identity.Pages.Account
{
    /// <summary>
    /// Register model controller
    /// </summary>
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly IEmailSender _emailSender;
        private readonly ILogger<RegisterModel> _logger;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        /// <summary>
        /// Register model controller constructor
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="logger"></param>
        /// <param name="emailSender"></param>
        public RegisterModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        /// <summary>
        /// Input
        /// </summary>
        [BindProperty] public InputModel Input { get; set; } = default!;

        /// <summary>
        /// Return url
        /// </summary>
        public string? ReturnUrl { get; set; }

        /// <summary>
        /// External logins
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; } = default!;

        /// <summary>
        /// Page init data
        /// </summary>
        /// <param name="returnUrl"></param>
        public async Task OnGetAsync(string? returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        /// <summary>
        /// Handle user inputs
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new AppUser {UserName = Input.UserName, Email = Input.Email, Firstname = Input.FirstName, Lastname = Input.LastName};
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        null,
                        new {area = "Identity", userId = user.Id, code},
                        Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                        return RedirectToPage("RegisterConfirmation", new {email = Input.Email});
                    await _signInManager.SignInAsync(user, false);
                    return LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors) ModelState.AddModelError(string.Empty, error.Description);
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
            /// Email
            /// </summary>
            [Required(ErrorMessageResourceType = typeof(Resources.Base.Common),
                ErrorMessageResourceName = "ErrorMessage_Required")]
            [EmailAddress(ErrorMessageResourceType = typeof(Resources.Base.Common),
                ErrorMessageResourceName = "ErrorMessage_Email")]
            [Display(ResourceType = typeof(Register),
                Name = nameof(Email))]
            public string Email { get; set; } = default!;
            
            /// <summary>
            /// Username
            /// </summary>
            [MaxLength(128, ErrorMessageResourceType = typeof(Resources.Base.Common),
                ErrorMessageResourceName = "ErrorMessage_MaxLength")] 
            [Display(ResourceType = typeof(Register),
                Name = "UserName")]
            public string UserName { get; set; } = default!;
            
            /// <summary>
            /// Firstname
            /// </summary>
            [MaxLength(128, ErrorMessageResourceType = typeof(Resources.Base.Common),
                ErrorMessageResourceName = "ErrorMessage_MaxLength")] 
            [Display(ResourceType = typeof(Register),
                Name = "FirstName")]
            public string FirstName { get; set; } = default!;
            
            /// <summary>
            /// Lastname
            /// </summary>
            [MaxLength(128, ErrorMessageResourceType = typeof(Resources.Base.Common),
                ErrorMessageResourceName = "ErrorMessage_MaxLength")] 
            [Display(ResourceType = typeof(Register),
                Name = "LastName")]
            public string LastName { get; set; } = default!;
            
            /// <summary>
            /// Password
            /// </summary>
            [Required(ErrorMessageResourceType = typeof(Resources.Base.Common),
                ErrorMessageResourceName = "ErrorMessage_Required")]
            [StringLength(100, ErrorMessageResourceType = typeof(Resources.Base.Common),
                ErrorMessageResourceName = "ErrorMessage_StringLengthMinMax", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = nameof(Password),
                ResourceType = typeof(Register))]
            public string Password { get; set; } = default!;

            
            /// <summary>
            /// Password cofirmation
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = nameof(ConfirmPassword), ResourceType = typeof(Register))] 
            [Compare("Password",
                ErrorMessageResourceType = typeof(Resources.Base.Areas.Identity.Pages.Account.Register),
                ErrorMessageResourceName = "PasswordsDontMatch")]
            public string ConfirmPassword { get; set; } = default!;
        }
    }
}