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

        public ActionResult Inquirer()
        {
            return View();
        }

        public PartialViewResult InquirerForm()
        {
            Inquirer inquirer = new Inquirer();
            inquirer.Name = "Country and color";
            inquirer.TextInput = "Where are you from?";
            inquirer.Radio = "What is your favorite color?+Red+Blue+Black";
            return PartialView(inquirer);
        }

        public PartialViewResult MainInquirerForm()
        {
            Inquirer inquirer = new Inquirer();
            inquirer.Name = "Where are you from?";
            inquirer.Radio = "Where are you from?+Ukraine+Hungary+Another planet";
            return PartialView(inquirer);
        }

        [HttpPost]
        public ActionResult InquirerForm(Inquirer inquirer)
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
            List<Inquirer> inquirers = repository.GetInquirerResults(inquirerName).ToList();
            return View(inquirers);
        }
    }
}