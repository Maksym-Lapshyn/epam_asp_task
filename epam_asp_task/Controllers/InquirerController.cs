using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using epam_asp_task.Infrastructure;
using epam_asp_task.Models;

namespace epam_asp_task.Controllers
{
    public class InquirerController : Controller
    {
        ProjectRepository repository = new ProjectRepository();

        [HttpGet]
        public ActionResult Inquirer()
        {
            return View(new Inquirer());
        }

        [HttpPost]
        public ActionResult Inquirer(Inquirer inquirer)
        {
            if (ModelState.IsValid)
            {
                TempData["Message"] = "Hurray! Your response on inquirer is added!";
                repository.AddInquirer(inquirer);
                return RedirectToAction("GetInquirerResults", new { inquirerName = inquirer.Name });
            }
            return View("Inquirer");
        }

        public ActionResult GetInquirerResults(string inquirerName)
        {
            IEnumerable<Inquirer> inquirerResults = repository.GetInquirerResults(inquirerName);
            return View(inquirerResults);
        }
    }
}