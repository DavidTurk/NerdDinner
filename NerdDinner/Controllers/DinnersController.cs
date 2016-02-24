using System;
using System.Collections.Generic;
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
                return View("Details", currentDinner);
        }

        // GET: Dinners/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dinners/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Dinners/Edit/5
        public ActionResult Edit(int id)
        {
            Dinner currentDinner = dinnerRepository.GetDinner(id);

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
                //TODO: ModelState.AddRuleViolations(currentDinner.GetRuleViolations());

                return View(currentDinner);
            }
        }

        // GET: Dinners/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Dinners/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
