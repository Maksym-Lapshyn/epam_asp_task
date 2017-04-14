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

        [HttpGet]
        public ActionResult InquirerForm()
        {
            InquirerViewModel viewModel = new InquirerViewModel();
            viewModel.Name = "Country and color";
            viewModel.TextQuestion = "Where are you from?";
            viewModel.RadioQuestion = "What is your favorite color?";
            viewModel.RadioOptions = InquirerRadioOptions;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult InquirerForm(InquirerViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                if (viewModel.HasRadio)
                {
                    viewModel.RadioOptions = InquirerRadioOptions;
                }
                return View(viewModel);
            }

            TempData["Message"] = "Hurray! Your response on inquirer is added!";
            bl.SaveInquirer(viewModel);
            return RedirectToAction("GetInquirerResults", new { inquirerName = viewModel.Name });
        }

        public ActionResult GetInquirerResults(string inquirerName)
        {
            List<Inquirer> inquirers = bl.GetInquirerResults(inquirerName).ToList();
            return View(inquirers);
        }
    }
}