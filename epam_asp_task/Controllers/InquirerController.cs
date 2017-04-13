using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using epam_asp_task.Infrastructure;
using epam_asp_task.Models;
using epam_asp_task.ViewModels;

namespace epam_asp_task.Controllers
{
    public class InquirerController : Controller
    {
        BusinessLogic bl = new BusinessLogic();

        private string[] InquirerRadioOptions = new[] { "Red", "Blue", "Black" };

        private string[] MainRadioOptions = new [] { "Batman Begins", "The Dark Knight", "The Dark Knight Rises" };

        [HttpGet]
        public ActionResult InquirerForm()
        {
            InquirerViewModel inquirer = new InquirerViewModel();
            inquirer.Name = "Country and color";
            inquirer.TextQuestion = "Where are you from?";
            inquirer.RadioQuestion = "What is your favorite color?";
            inquirer.RadioOptions = InquirerRadioOptions;
            return View(inquirer);
        }

        [HttpPost]
        public ActionResult InquirerForm(InquirerViewModel inquirer)
        {
            if (!ModelState.IsValid)
            {
                if(inquirer.RadioInput != InquirerViewModel.EmptyInput)
                {
                    inquirer.RadioOptions = InquirerRadioOptions;
                }
                return View(inquirer);
            }

            TempData["Message"] = "Hurray! Your response on inquirer is added!";
            Inquirer inquirerForSave = new Inquirer();
            inquirerForSave.Name = inquirer.Name;
            inquirerForSave.TextInput = inquirer.TextInput;
            inquirerForSave.Radio = inquirer.RadioInput;
            bl.AddInquirer(inquirerForSave);
            return RedirectToAction("GetInquirerResults", new { inquirerName = inquirer.Name }); 
        }

        [HttpGet]
        public PartialViewResult MainInquirerForm()
        {
            InquirerViewModel inquirer = new InquirerViewModel();
            inquirer.Name = "Nolan's Batman";
            inquirer.TextInput = InquirerViewModel.EmptyInput;
            inquirer.RadioQuestion = "What is your favorite Nolan's Batman?";
            inquirer.RadioOptions = new [] { "Batman Begins", "The Dark Knight", "The Dark Knight Rises" };
            return PartialView(inquirer);
        }

        public ActionResult GetInquirerResults(string inquirerName)
        {
            List<Inquirer> inquirers = bl.GetInquirerResults(inquirerName).ToList();
            return View(inquirers);
        }
    }
}