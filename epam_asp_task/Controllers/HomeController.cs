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
    public class HomeController : Controller
    {
        BusinessLogic bl = new BusinessLogic();

        private string[] InquirerRadioOptions = new[] { "Batman Begins", "The Dark Knight", "The Dark Knight Rises" };

        [HttpGet]
        public ActionResult Index()
        {
            //if db is empty
            if (bl.Articles.Count() == 0)
            {
                GenerateContent();
            }

            List <Article> smallArticles = bl.GetSmallArticles();
            return View(smallArticles);
        }

        [HttpGet]
        public ActionResult Search()
        {
            return PartialView();
        }

        [HttpPost]
        public JsonResult Search(string prefix)
        {
            var articles = bl.Articles.Where(a => a.Name.StartsWith(prefix)).Select(a => new { label = a.Name, val = a.Id });
            return Json(articles, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public PartialViewResult IndexInquirerForm()
        {
            InquirerViewModel viewModel = new InquirerViewModel();
            viewModel.Name = "Nolan's Batman";
            viewModel.HasText = false;
            viewModel.RadioQuestion = "What is your favorite Nolan's Batman?";
            viewModel.RadioOptions = new[] { "Batman Begins", "The Dark Knight", "The Dark Knight Rises" };
            return PartialView(viewModel);
        }

        [HttpPost]
        public PartialViewResult IndexInquirerForm(InquirerViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                if (viewModel.HasRadio)
                {
                    viewModel.RadioOptions = InquirerRadioOptions;
                }

                return PartialView(viewModel);
            }

            TempData["Message"] = "Hurray! Your response on inquirer is added!";
            bl.SaveInquirer(viewModel);
            List<Inquirer> inquirers = bl.GetInquirerResults(viewModel.Name).ToList();
            return PartialView("GetIndexInquirerResults", inquirers);
        }

        private void GenerateContent()
        {
            Article firstArticle = new Article();
            firstArticle.Name = "First Article of this Blog";
            firstArticle.PublicationDate = DateTime.UtcNow;
            firstArticle.Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum";
            firstArticle.Keywords = "#cool #story #blog #yo #girls #beach";
            bl.SaveArticle(firstArticle);

            Article secondArticle = new Article();
            secondArticle.Name = "Second Article of this Blog";
            secondArticle.PublicationDate = DateTime.UtcNow;
            secondArticle.Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum";
            secondArticle.Keywords = "#hot #girls #on #the #beach";
            bl.SaveArticle(secondArticle);

            Feedback firstFeedback = new Feedback();
            firstFeedback.Author = "good_user_5151";
            firstFeedback.PublicationDate = DateTime.UtcNow;
            firstFeedback.Content = "damn, this website is pretty good!";
            bl.AddFeedback(firstFeedback);

            Feedback secondFeedback = new Feedback();
            secondFeedback.Author = "bad_user_666";
            secondFeedback.PublicationDate = DateTime.UtcNow;
            secondFeedback.Content = "what a bullsh!t, the author of this garbage is a moron!";
            bl.AddFeedback(secondFeedback);

            /*Inquirer firstInquirer = new Inquirer();
            firstInquirer.Name = "Place of Origin Inquirer";
            firstInquirer.TextInput = "Kharkiv";
            bl.AddInquirer(firstInquirer);

            Inquirer secondInquirer = new Inquirer();
            secondInquirer.Name = "Place of Origin Inquirer";
            secondInquirer.TextInput = "Kharkiv";
            bl.AddInquirer(secondInquirer);

            Inquirer thirdInquirer = new Inquirer();
            thirdInquirer.Name = "Place of Origin Inquirer";
            thirdInquirer.TextInput = "Kyiv";
            bl.AddInquirer(thirdInquirer);

            Inquirer fourthInquirer = new Inquirer();
            fourthInquirer.Name = "Place of Origin Inquirer";
            fourthInquirer.TextInput = "Kyiv";
            bl.AddInquirer(fourthInquirer);

            Inquirer fifthInquirer = new Inquirer();
            fifthInquirer.Name = "Place of Origin Inquirer";
            fifthInquirer.TextInput = "Poltava";
            bl.AddInquirer(fifthInquirer);*/
        }
    }
}