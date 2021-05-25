using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;
using Extensions.Base;
using Microsoft.AspNetCore.Authorization;
using WebApp.Helpers;
using WebApp.ViewModels.Contacts;

namespace WebApp.Controllers
{
    [Authorize]
    public class ContactsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public ContactsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Contacts
        public async Task<IActionResult> Index()
        {
            var res = await _uow.Contacts.GetAllAsync(User.GetUserId()!.Value);
            return View(res);
        }

        // GET: Contacts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _uow.Contacts.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);

            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // GET: Contacts/Create
        public async Task<IActionResult> Create()
        {
            var vm = new ContactCreateEditViewModel();
            vm.ContactTypeSelectList = new SelectList(await _uow.ContactTypes.GetAllAsync(), nameof(ContactType.Id),
                nameof(ContactType.Type));
            vm.PersonSelectList =  new SelectList(await _uow.Persons.GetAllAsync(User.GetUserId()!.Value),
                nameof(Person.Id), nameof(Person.FullName));
            return View(vm);
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContactCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _uow.Contacts.Add(vm.Contact);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            vm.ContactTypeSelectList = new SelectList(await _uow.ContactTypes.GetAllAsync(), nameof(ContactType.Id),
                nameof(ContactType.Type), vm.Contact.ContactTypeId);
            vm.PersonSelectList =  new SelectList(await _uow.Persons.GetAllAsync(User.GetUserId()!.Value),
                nameof(Person.Id), nameof(Person.FullName), vm.Contact.PersonId);


            return View(vm);
        }

        // GET: Contacts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _uow.Contacts.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (contact == null)
            {
                return NotFound();
            }
            var vm = new ContactCreateEditViewModel();
            vm.Contact = contact;
            vm.ContactTypeSelectList = new SelectList(await _uow.ContactTypes.GetAllAsync(), nameof(ContactType.Id),
                nameof(ContactType.Type), vm.Contact.ContactTypeId);
            vm.PersonSelectList =  new SelectList(await _uow.Persons.GetAllAsync(User.GetUserId()!.Value),
                nameof(Person.Id), nameof(Person.FullName), vm.Contact.PersonId);
            return View(vm);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ContactCreateEditViewModel vm)
        {
            if (id != vm.Contact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.Contacts.Update(vm.Contact);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            vm.ContactTypeSelectList = new SelectList(await _uow.ContactTypes.GetAllAsync(), nameof(ContactType.Id),
                nameof(ContactType.Type), vm.Contact.ContactTypeId);
            vm.PersonSelectList =  new SelectList(await _uow.Persons.GetAllAsync(User.GetUserId()!.Value),
                nameof(Person.Id), nameof(Person.FullName), vm.Contact.PersonId);

            return View(vm);
        }

        // GET: Contacts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _uow.Contacts.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Contacts.RemoveAsync(id, User.GetUserId()!.Value);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}