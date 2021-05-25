using System;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Domain.App.Constants;
using Domain.App.Identity;
using Extensions.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Areas.Admin.ViewModels.DrivingSchool;

namespace WebApp.Areas.Admin.Controllers
{
    /// <summary>
    ///     Controller with full CRUD functionality for managing Driving school objects.
    /// </summary>
    [Area(nameof(WebApp.Areas.Admin))]
    [Route("Admin/DrivingSchools/{action=index}/{username?}")]
    [Authorize(Roles = AppRoles.Administrator)]
    public class DrivingSchoolsController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<Domain.App.Identity.AppUser> _userManager;


        /// <summary>
        ///     Constructor for Driving school controller
        /// </summary>
        /// <param name="bll">Business logic layer</param>
        /// <param name="userManager"></param>
        public DrivingSchoolsController(IAppBLL bll, UserManager<AppUser> userManager)
        {
            _bll = bll;
            _userManager = userManager;
        }

        // GET: DrivingSchools
        /// <summary>
        ///     Main page of Driving school
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return View(await _bll.DrivingSchools.GetAllAsync());

        }

        // GET: DrivingSchools/Create
        /// <summary>
        ///     Data for driving school creation
        /// </summary>
        /// <returns>DrivingSchoolCreateEditViewModel</returns>
        private async Task<IActionResult> Create()
        {
            var vm = new DrivingSchoolCreateEditViewModel();
            var query = _userManager.Users.OrderBy(u => u.UserName);
            vm.AppUserSelectList = new SelectList(await query.ToArrayAsync(),
                nameof(AppUser.Id),
                nameof(AppUser.UserName));
            return View(vm);
        }

        // POST: DrivingSchools/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        ///     Driving school creation handling
        /// </summary>
        /// <param name="vm">DrivingSchoolCreateEditViewModel</param>
        /// <returns>DrivingSchoolCreateEditViewModel</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DrivingSchoolCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _bll.DrivingSchools.Add(vm.DrivingSchool);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Console.WriteLine(Debugging.ErrorMessages.GetModelStateErrors(ModelState));
            }

            var query = _userManager.Users.OrderBy(u => u.UserName);

            vm.AppUserSelectList = new SelectList(await query.ToArrayAsync(),
                nameof(AppUser.Id),
                nameof(AppUser.UserName),
                vm.DrivingSchool.AppUserId);
            
            return View(vm);
        }

        // GET: DrivingSchools/Edit/5
        /// <summary>
        ///     Data for driving school editing
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drivingSchool = await _bll.DrivingSchools.FirstOrDefaultAsync(id.Value);

            if (drivingSchool == null)
            {
                return NotFound();
            }

            var vm = new DrivingSchoolCreateEditViewModel();
            vm.DrivingSchool = drivingSchool;
            vm.AppUserSelectList = new SelectList(await _userManager.Users.OrderBy(u => u.UserName).ToListAsync(),
                nameof(AppUser.Id),
                nameof(AppUser.UserName),
                vm.DrivingSchool.AppUserId);

            return View(vm);
        }

        // POST: DrivingSchools/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        ///     Driving school editing handling
        /// </summary>
        /// <param name="vm">DrivingSchoolCreateEditViewModel</param>
        /// <returns>DrivingSchoolCreateEditViewModel</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DrivingSchoolCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var newOwnerUser = _userManager.Users.FirstOrDefaultAsync(user => user.Id == vm.DrivingSchool.AppUserId).Result;
                await _userManager.AddToRoleAsync(newOwnerUser, "owner");
                _bll.DrivingSchools.Update(vm.DrivingSchool);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            vm.AppUserSelectList = new SelectList(await _userManager.Users.OrderBy(u => u.UserName).ToListAsync(),
                nameof(AppUser.Id),
                nameof(AppUser.UserName),
                vm.DrivingSchool.AppUserId);

            return View(vm);
        }


        // POST: DrivingSchools/Delete/5
        /// <summary>
        ///     Driving school deletion handling
        /// </summary>
        /// <param name="id">Driving school ID</param>
        /// <returns>Redirect to Index page</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.DrivingSchools.RemoveAsync(id, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DrivingSchoolExists(Guid id)
        {
            return _bll.DrivingSchools.ExistsAsync(id).Result;

        }
    }
}
