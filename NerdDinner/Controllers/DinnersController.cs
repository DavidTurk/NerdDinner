using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NerdDinner.Models;
using PagedList;

namespace NerdDinner.Controllers
{
    public class DinnersController : Controller
    {
        private DinnerRepository dinnerRepository = new DinnerRepository();
        // GET: Dinners
        public ActionResult Index(string currentFilter,string searchString, int? page, int pageSize = 10)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            int pageNumber = (page ?? 1);
            var upcomingDinners = dinnerRepository.FindUpcomingDinners().ToList();
            return View("Index", upcomingDinners.ToPagedList(pageNumber, pageSize));
        }

        // GET: Dinners/Details/5
        public ActionResult Details(int id)
        {
            Dinner currentDinner = dinnerRepository.GetDinner(id);

            if (currentDinner == null)
                return View("NotFound");
            else
                return View(currentDinner);
        }

        // GET: Dinners/Create
        [Authorize]
        public ActionResult Create()
        {
            Dinner currentDinner = new Dinner()
            {
                EventDate = DateTime.Now.AddDays(7)
            };
            
            return View(new DinnerFormViewModel(currentDinner));
        }

        // POST: Dinners/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create(Dinner dinner)
        {
            if (ModelState.IsValid)
            {
                dinner.HostedBy = User.Identity.Name;

                RSVP rsvp = new RSVP {AttendeeName = User.Identity.Name};

                dinner.RSVPs = new List<RSVP>();
                dinner.RSVPs.Add(rsvp);
                
                dinnerRepository.Add(dinner);
                dinnerRepository.Save();

                return RedirectToAction("Index");
            }

            return View(new DinnerFormViewModel(dinner));
        }

        // GET: Dinners/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            Dinner currentDinner = dinnerRepository.GetDinner(id);
            
            if (currentDinner == null)
                return View("NotFound");

            if (!currentDinner.IsHostedBy(User.Identity.Name))
                return View("InvalidOwner");

            return View(new DinnerFormViewModel(currentDinner));
        }

        // POST: Dinners/Edit/5
        [Authorize]
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            Dinner currentDinner = dinnerRepository.GetDinner(id);

            if (!currentDinner.IsHostedBy(User.Identity.Name))
                return View("InvalidOwner");

            try
            {
                UpdateModel(currentDinner);

                dinnerRepository.Save();

                return RedirectToAction("Details", new {id = currentDinner.DinnerID});
            }
            catch
            {
                return View(new DinnerFormViewModel(currentDinner));
            }
        }

        // GET: Dinners/Delete/5
        public ActionResult Delete(int id)
        {
            Dinner dinner = dinnerRepository.GetDinner(id);

            if (dinner == null)
                return View("NotFound");

            if (!dinner.IsHostedBy(User.Identity.Name))
                return View("InvalidOwner");

            return View(dinner);
        }

        // POST: Dinners/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            Dinner dinner = dinnerRepository.GetDinner(id);

            if (dinner == null)
                return View("NotFound");

            if (!dinner.IsHostedBy(User.Identity.Name))
                return View("InvalidOwner");

            dinnerRepository.Delete(dinner);
            dinnerRepository.Save();

            return View("Deleted");
        }
    }
}
