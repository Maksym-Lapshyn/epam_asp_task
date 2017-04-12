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
        BusinessLogic bl = new BusinessLogic();

        [HttpGet]
        public ActionResult InquirerForm()
        {
            Inquirer inquirer = new Inquirer();
            inquirer.Name = "Country and color";
            inquirer.TextInput = "Where are you from?";
            inquirer.Radio = "What is your favorite color?+Red+Blue+Black";
            return View(inquirer);
        }

        [HttpPost]
        public ActionResult InquirerForm(Inquirer inquirer)
        {
            if (!ModelState.IsValid)
            {
                return View(inquirer);
            }

            TempData["Message"] = "Hurray! Your response on inquirer is added!";
            bl.AddInquirer(inquirer);
            return RedirectToAction("GetInquirerResults", new { inquirerName = inquirer.Name }); 
        }

        public PartialViewResult MainInquirerForm()
        {
            Inquirer inquirer = new Inquirer();
            inquirer.Name = "Where are you from?";
            inquirer.TextInput = "empty";
            inquirer.Radio = "Where are you from?+Ukraine+Hungary+Another planet";
            return PartialView(inquirer);
        }

        public ActionResult GetInquirerResults(string inquirerName)
        {
            List<Inquirer> inquirers = bl.GetInquirerResults(inquirerName).ToList();
            return View(inquirers);
        }
    }
}