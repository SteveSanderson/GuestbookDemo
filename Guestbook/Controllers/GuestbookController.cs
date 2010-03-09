using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Guestbook.Domain.Entities;
using Guestbook.Domain.Repositories;

namespace Guestbook.Controllers
{
    public class GuestbookController : Controller
    {
        private readonly IGuestbookEntryRepository _repository = IoC.CurrentGuestbookEntryRepository;

        public ActionResult Index()
        {
            return View(_repository.Entries.OrderByDescending(x => x.PostedDate));
        }

        public ActionResult Post()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Post(GuestbookEntry entry)
        {
            TempData["message"] = "Thanks for posting!";
            _repository.AddEntry(entry);
            return RedirectToAction("Index");
        }
    }
}
