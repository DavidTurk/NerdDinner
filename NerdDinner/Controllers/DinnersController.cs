using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NerdDinner.Models;

namespace NerdDinner.Controllers
{
    public class DinnersController : Controller
    {
        private DinnerRepository dinnerRepository = new DinnerRepository();
        // GET: Dinners
        public ActionResult Index()
        {
            var upcomingDinners = dinnerRepository.FindUpcomingDinners().ToList();
            return View("Index", upcomingDinners);
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
        public ActionResult Create()
        {
            Dinner currentDinner = new Dinner()
            {
                EventDate = DateTime.Now.AddDays(7)
            };
            
            return View(currentDinner);
        }

        // POST: Dinners/Create
        [HttpPost]
        public ActionResult Create(Dinner dinner)
        {
            if (ModelState.IsValid)
            {
                
                dinnerRepository.Add(dinner);
                dinnerRepository.Save();

                return RedirectToAction("Index");
            }

            return View(dinner);
        }

        // GET: Dinners/Edit/5
        public ActionResult Edit(int id)
        {
            Dinner currentDinner = dinnerRepository.GetDinner(id);
            
            if (currentDinner == null)
                return View("NotFound");

            return View(currentDinner);
        }

        // POST: Dinners/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            Dinner currentDinner = dinnerRepository.GetDinner(id);

            try
            {
                UpdateModel(currentDinner);

                dinnerRepository.Save();

                return RedirectToAction("Details", new {id = currentDinner.DinnerID});
            }
            catch
            {
                return View(currentDinner);
            }
        }

        // GET: Dinners/Delete/5
        public ActionResult Delete(int id)
        {
            Dinner dinner = dinnerRepository.GetDinner(id);

            if (dinner == null)
                return View("NotFound");
            else
                return View(dinner);
        }

        // POST: Dinners/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            Dinner dinner = dinnerRepository.GetDinner(id);

            if (dinner == null)
                return View("NotFound");

            dinnerRepository.Delete(dinner);
            dinnerRepository.Save();

            return View("Deleted");
        }
    }
}
