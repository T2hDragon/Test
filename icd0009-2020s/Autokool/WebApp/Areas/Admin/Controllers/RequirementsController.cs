using System;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Domain.App.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Areas.Admin.Controllers
{
    /// <summary>
    ///     Controller with full CRUD functionality for managing Requirement objects.
    /// </summary>
    [Area(nameof(WebApp.Areas.Admin))]
    [Route("Admin/Requirements/{action=index}/{username?}")]
    [Authorize(Roles = AppRoles.Administrator)]
    public class RequirementsController : Controller
    {
        private readonly IAppBLL _bll;

        /// <summary>
        ///     Constructor for Requirements controller
        /// </summary>
        /// <param name="bll">Business logic layer</param>
        public RequirementsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Requirement
        /// <summary>
        ///     Main page of Requirements
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return View(await _bll.Requirements.GetAllAsync());
        }
        

        // GET: Requirement/Create
        /// <summary>
        ///     Data for Requirement creation
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        // POST: Requirement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        ///     Requirement creation handling
        /// </summary>
        /// <param name="requirement">Requirement DTO</param>
        /// <returns>Redirect</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description,Name,Price,Id")] BLL.App.DTO.Requirement requirement)
        {
            if (ModelState.IsValid)
            {
                _bll.Requirements.Add(requirement);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(requirement);
        }

        // GET: Requirement/Edit/5
        /// <summary>
        ///     Data for Requirement editing
        /// </summary>
        /// <param name="id">Requirement Id</param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requirement = await _bll.Requirements.FirstOrDefaultAsync(id.Value);

            if (requirement == null)
            {
                return NotFound();
            }

            return View(requirement);
        }

        // POST: Requirement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        ///     Requirement editing handling
        /// </summary>
        /// <param name="id">Requirement ID</param>
        /// <param name="requirement">Requirement DTO</param>
        /// <returns>Redirect</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Description,Name,Price,Id")] BLL.App.DTO.Requirement requirement)
        {
            if (!ModelState.IsValid || !await _bll.Requirements.ExistsAsync(requirement.Id)) {
                return View(requirement);
            }

            _bll.Requirements.Update(requirement);
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



        // POST: Requirement/Delete/5
        /// <summary>
        ///     Requirement deletion handling
        /// </summary>
        /// <param name="id">Delete requirement ID</param>
        /// <returns>Redirect</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Requirements.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequirementExists(Guid id)
        {
            return _bll.Requirements.ExistsAsync(id).Result;
        }
    }
}
