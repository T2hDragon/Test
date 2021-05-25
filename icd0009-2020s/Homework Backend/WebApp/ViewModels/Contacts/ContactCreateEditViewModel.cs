using DAL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels.Contacts
{
    public class ContactCreateEditViewModel
    {
        public Contact Contact { get; set; } = default!;

        public SelectList? ContactTypeSelectList { get; set; }
        public SelectList? PersonSelectList { get; set; }
    }
}