using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using DAL.App.EF;
using BLL.App.DTO;
using Domain.App.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Areas.Admin.Controllers
{
    /// <summary>
    ///     Controller with full CRUD functionality for managing Lesson objects.
    /// </summary>
    [Area(nameof(WebApp.Areas.Admin))]
    [Route("Admin/Lessons/{action=index}")]
    [Authorize(Roles = AppRoles.Administrator)]
    public class LessonsController : Controller
    {
        private readonly IAppBLL _bll;

        /// <summary>
        ///     Constructor for Lesson controller
        /// </summary>
        /// <param name="bll">Business logic layer</param>
        public LessonsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Lessons
        /// <summary>
        ///     Main page of Lessons
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return View(await _bll.Lessons.GetAllAsync());
        }
        

        // GET: Lessons/Create
        /// <summary>
        ///     Data for lesson creation
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lessons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        ///     Driving lesson handling
        /// </summary>
        /// <param name="lesson">Lesson DTO</param>
        /// <returns>Lesson DTO</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Start,End,Id")] Lesson lesson)
        {
            if (ModelState.IsValid)
            {
                lesson.Id = Guid.NewGuid();
                _bll.Lessons.Add(lesson);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lesson);
        }

        // GET: Lessons/Edit/5
        /// <summary>
        ///     Data for lesson editing
        /// </summary>
        /// <param name="id">Lesson Id</param>
        /// <returns>Lesson</returns>
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lesson = await _bll.Lessons.FirstOrDefaultAsync(id.Value);
            if (lesson == null)
            {
                return NotFound();
            }
            return View(lesson);
        }

        // POST: Lessons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        ///     Lesson editing handling
        /// </summary>
        /// <param name="id">Lesson Id</param>
        /// <param name="lesson">Lesson DTO</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Lesson lesson)
        {
            Console.WriteLine(lesson.Start.ToString());
            Console.WriteLine(lesson.End.ToString());
            Console.WriteLine(lesson.Id.ToString());
            if (id != lesson.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bll.Lessons.Update(lesson);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await LessonExists(lesson.Id))
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Console.WriteLine(Debugging.ErrorMessages.GetModelStateErrors(ModelState));
            }
            return View(lesson);
        }

        // POST: Lessons/Delete/5
        /// <summary>
        ///     Lesson deletion handling
        /// </summary>
        /// <param name="id">lesson ID</param>
        /// <returns>Redirect to Index page</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var lesson = await _bll.Lessons.FirstOrDefaultAsync(id);
            if (lesson != null)
            {
                _bll.Lessons.Remove(lesson);
                await _bll.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> LessonExists(Guid id)
        {
            return await _bll.Lessons.ExistsAsync(id);
        }
    }
}
