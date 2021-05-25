using System;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Domain.App.Constants;
using Domain.App.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Areas.Admin.ViewModels;
using WebApp.Areas.Admin.ViewModels.AppUser;

namespace WebApp.Areas.Admin.Controllers
{
    /// <summary>
    ///     Controller with full CRUD functionality for managing AppUser objects.
    /// </summary>
    [Area(nameof(WebApp.Areas.Admin))]
    [Route("Admin/AppUsers/{action=index}/{username?}")]
    [Authorize(Roles = AppRoles.Administrator)]
    public class AppUsersController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<Domain.App.Identity.AppUser> _userManager;

        /// <summary>
        ///     Admin views controller construction
        /// </summary>
        /// <param name="userManager">User manager</param>
        /// <param name="roleManager">Role manager</param>
        public AppUsersController(UserManager<Domain.App.Identity.AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        /// <summary>
        ///     Index page action
        /// </summary>
        /// <param name="search">User search string</param>
        /// <param name="includeDeactivated">Asks if wants to show deactivated accounts</param>
        /// <returns>AppUsersIndexViewModel</returns>
        public async Task<IActionResult> Index(string? search, bool? includeDeactivated)
        {
            search = search?.ToUpper();
            var query = _userManager.Users.Where(user =>
                (search == null || user.NormalizedUserName.Contains(search) || user.NormalizedEmail.Contains(search))
                && (includeDeactivated == true || !user.AccountLocked));

            var foundUsers = await query.OrderBy(user => user.CreatedAt).ToListAsync();

            var searchViewModel = new AppUserSearchViewModel
            {
                search = search ?? "",
                includeDeactivated = includeDeactivated ?? false
            };
            var userViewModels = foundUsers.Select(user => new AppUsersViewModel
            {
                Email = user.Email,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                UserName = user.UserName,
                IsLocked = user.AccountLocked
            });
            var viewModel = new AppUsersIndexViewModel
            {
                AppUsers = userViewModels,
                SearchViewModel = searchViewModel
            };

            return View(viewModel);
        }

        // GET: Admin/Users/Details/5
        /// <summary>
        ///     Details page action
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>AppUserDetailsViewModel</returns>
        public async Task<IActionResult> Details(string? userName)
        {
            if (userName == null) return NotFound();

            var appUser = await _userManager.FindByNameAsync(userName);
            if (appUser == null) return NotFound();
            var viewModel = new AppUserDetailsViewModel
            {
                CreatedAt = appUser.CreatedAt,
                Email = appUser.Email,
                EmailConfirmed = appUser.EmailConfirmed,
                LockoutEnabled = appUser.LockoutEnabled,
                PhoneNumber = appUser.PhoneNumber ?? "Unknown",
                PhoneNumberConfirmed = appUser.PhoneNumberConfirmed,
                UserName = appUser.UserName,
                FirstName = appUser.Firstname,
                LastName = appUser.Lastname
            };
            return View(viewModel);
        }

        // GET: Admin/Users/Create
        /// <summary>
        ///  Creation page action
        /// </summary>
        /// <returns>AppUserCreateManageViewModel</returns>
        public async Task<IActionResult> Create()
        {
            var viewModel = new AppUserCreateManageViewModel
            {
                UserName = "",
                Firstname = "",
                Lastname = "",
                Password = "",
                Roles = await GetRolesSelectListAsync()
            };
            return View(viewModel);
        }

        /// <summary>
        ///     Create page post handling
        /// </summary>
        /// <param name="createViewModel">User creation view model</param>
        /// <returns>AppUserCreateManageViewModel</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppUserCreateManageViewModel createViewModel)
        {
            if (!await IsUserNameValidAsync(createViewModel.UserName))
                ModelState.AddModelError("", "This username is already in use!");
            if (!await IsEmailValidAsync(createViewModel.Email))
                ModelState.AddModelError("", "This email is already in use!");

            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = createViewModel.UserName,
                    Firstname = createViewModel.Firstname,
                    Lastname = createViewModel.Lastname,
                    Email = createViewModel.Email
                };
                var userAddResult = await _userManager.CreateAsync(user, createViewModel.Password);
                if (userAddResult == IdentityResult.Success)
                {
                    var userInUserManager = await _userManager.FindByEmailAsync(user.Email);
                    var rolesAddResult =
                        await _userManager.AddToRolesAsync(userInUserManager, createViewModel.SelectedRoles);
                    if (rolesAddResult.Succeeded && userAddResult.Succeeded) return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "User creation failed");
                }
            }

            createViewModel.Roles = await GetRolesSelectListAsync();
            return View(createViewModel);
        }

        /// <summary>
        ///     User managing page action
        /// </summary>
        /// <param name="userName">User username</param>
        /// <returns>AppUserCreateManageViewModel</returns>
        public async Task<IActionResult> ManageUser(string? userName)
        {
            if (userName == null) return NotFound();

            var appUser = await _userManager.FindByNameAsync(userName);

            if (appUser == null) return NotFound();

            var viewModel = new AppUserCreateManageViewModel
            {
                Email = appUser.Email,
                UserName = appUser.UserName,
                Firstname = appUser.Firstname,
                Lastname = appUser.Lastname,
                SelectedRoles = await _userManager.GetRolesAsync(appUser),
                Roles = await GetRolesSelectListAsync()
            };
            return View(viewModel);
        }

        /// <summary>
        ///     User manager post action
        /// </summary>
        /// <param name="viewModel">App user edit handling view model.</param>
        /// <returns>AppUserCreateManageViewModel</returns>
        [HttpPost]
        public async Task<IActionResult> ManageUser(AppUserCreateManageViewModel viewModel)
        {
            var appUser = await _userManager.FindByNameAsync(viewModel.UserName);
            
            if (appUser == null) return NotFound();

            foreach (var role in AppRoles.AllRoles)
            {
                // RemoveFromRolesAsync does not work properly
                await _userManager.RemoveFromRoleAsync(appUser, role);
            }

            if (viewModel.SelectedRoles.Any())
            {
                await _userManager.AddToRolesAsync(appUser, viewModel.SelectedRoles);
            }
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        ///     Alert for locking user
        /// </summary>
        /// <param name="userName">Locking user username</param>
        /// <param name="returnUrl">Url to return to</param>
        /// <returns>return url</returns>
        [HttpPost]
        public async Task<IActionResult> LockUser(string userName, string returnUrl)
        {
            if (string.IsNullOrEmpty(userName)) return BadRequest();
            var appUser = await _userManager.FindByNameAsync(userName);
            if (appUser == null) return BadRequest();
            await _userManager.SetLockoutEnabledAsync(appUser, true);

            await _userManager.SetLockoutEndDateAsync(appUser, DateTime.Today.AddYears(500));
            appUser.AccountLocked = true;
            await _userManager.UpdateAsync(appUser);
            return LocalRedirect(returnUrl);
        }

        /// <summary>
        ///     Alert for unlocking user
        /// </summary>
        /// <param name="userName">Locking user username</param>
        /// <param name="returnUrl">Url to return to</param>
        /// <returns>return url</returns>
        [HttpPost]
        public async Task<IActionResult> UnLockUser(string userName, string returnUrl)
        {
            if (string.IsNullOrEmpty(userName)) return BadRequest();

            var appUser = await _userManager.FindByNameAsync(userName);
            if (appUser == null) return BadRequest();
            await _userManager.SetLockoutEnabledAsync(appUser, false);
            appUser.AccountLocked = false;
            await _userManager.UpdateAsync(appUser);
            return LocalRedirect(returnUrl);
        }

        
        /// <summary>
        ///     Search page action
        /// </summary>
        /// <param name="viewModel">Page search view model</param>
        /// <returns>Redirect to controller index page</returns>
        [HttpPost]
        public IActionResult Search(AppUserSearchViewModel viewModel)
        {
            if (ModelState.IsValid)
                return RedirectToAction(nameof(Index),
                    new {Search = viewModel.search, IncludeDeactivated = viewModel.includeDeactivated});
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> IsEmailValidAsync(string email)
        {
            var userWithEmail =
                await _userManager.Users.FirstOrDefaultAsync(user => user.NormalizedEmail.Equals(email.ToUpper()));
            if (userWithEmail != null) return false;
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private async Task<bool> IsUserNameValidAsync(string username)
        {
            var userWithUsername =
                await _userManager.Users.FirstOrDefaultAsync(user =>
                    user.NormalizedUserName.Equals(username.ToUpper()));
            return userWithUsername == null;
        }

        private async Task<MultiSelectList> GetRolesSelectListAsync()
        {
            var appRoles = await _roleManager.Roles.ToListAsync();

            return new MultiSelectList(
                appRoles, nameof(AppRole.Name), nameof(AppRole.DisplayName));
        }
    }
}