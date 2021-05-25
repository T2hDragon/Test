using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using DAL.App.DTO;
using Extensions.Base;
using Microsoft.AspNetCore.Authorization;
using WebApp.Helpers;

namespace WebApp.Controllers
{
    [Authorize]
    public class SimplesController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public SimplesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Simples
        public async Task<IActionResult> Index()
        {
            var res = await _uow.Simples.GetAllAsync(User.GetUserId()!.Value);
            return View(res);
        }

        // GET: Simples/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exists = await _uow.Simples.ExistsAsync(id.Value, User.GetUserId()!.Value);
            if (!exists)
            {
                return NotFound();
            }
            
            var simple = await _uow.Simples.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);

            if (simple == null)
            {
                return NotFound();
            }

            return View(simple);
        }

        // GET: Simples/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Simples/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Simple simple)
        {
            if (ModelState.IsValid)
            {
                simple.AppUserId = User.GetUserId()!.Value;
                _uow.Simples.Add(simple);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(simple);
        }

        // GET: Simples/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var simple = await _uow.Simples.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
           
            if (simple == null)
            {
                return NotFound();
            }

            return View(simple);
        }

        // POST: Simples/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Simple simple)
        {
            if (id != simple.Id)
            {
                return NotFound();
            }

            // model must be valid and id must belong to current user
            if (!ModelState.IsValid || !await _uow.Simples.ExistsAsync(simple.Id, User.GetUserId()!.Value))
                return View(simple);
            simple.AppUserId = User.GetUserId()!.Value;
            
            _uow.Simples.Update(simple);
            await _uow.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        // GET: Simples/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var simple = await _uow.Simples.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            
            if (simple == null)
            {
                return NotFound();
            }

            return View(simple);
        }

        // POST: Simples/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Simples.RemoveAsync(id, User.GetUserId()!.Value);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}