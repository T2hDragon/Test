using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.DTO;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ContactTypesController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public ContactTypesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }


        // GET: ContactTypes
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var res = await _uow.ContactTypes.GetAllAsync();
            return View(res);
        }

        // GET: ContactTypes/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exists = await _uow.ContactTypes.ExistsAsync(id.Value, Guid.NewGuid());
            if (!exists )
            {
                return NotFound();
            }
            
            var contactType = await _uow.ContactTypes
                .FirstOrDefaultAsync(id.Value);

            if (contactType == null)
            {
                return NotFound();
            }

            return View(contactType);
        }

        // GET: ContactTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContactType contactType)
        {
            if (!ModelState.IsValid) return View(contactType);

            _uow.ContactTypes.Add(contactType);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: ContactTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactType = await _uow.ContactTypes.FirstOrDefaultAsync(id.Value);
            if (contactType == null)
            {
                return NotFound();
            }

            return View(contactType);
        }

        // POST: ContactTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ContactType contactType)
        {
            if (id != contactType.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(contactType);
            
            _uow.ContactTypes.Update(contactType);
            await _uow.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        // GET: ContactTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactType = await _uow.ContactTypes.FirstOrDefaultAsync(id.Value);
            
            if (contactType == null)
            {
                return NotFound();
            }

            return View(contactType);
        }

        // POST: ContactTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.ContactTypes.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}