using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using epam_asp_task.Models;

namespace epam_asp_task.Controllers
{
    public class HomeController : Controller
    {
        ProjectRepository repository = new ProjectRepository();

        public ActionResult Index()
        {
            //Already generated!
            //GenerateContent();
            IEnumerable<Article> articles = repository.Articles;
            return View(articles);
        }

        public ActionResult Feedback()
        {
            IEnumerable<Feedback> feedbacks = repository.Feedbacks;
            return View(feedbacks);
        }

        [HttpGet]
        public PartialViewResult FeedbackForm()
        {
            return PartialView(new Feedback());
        }

        [HttpPost]
        public ActionResult FeedbackForm(Feedback newFeedback)
        {
            if (ModelState.IsValid)
            {
                newFeedback.FeedbackPublicationDate = DateTime.UtcNow;
                TempData["Message"] = "Hurray! Your feedback is added!";
                repository.AddFeedback(newFeedback);
                return RedirectToAction("Feedback");
            }
            return View("FeedbackForm");
        }

        private void GenerateContent()
        {
            Article firstArticle = new Article();
            firstArticle.ArticleName = "First Article of this Blog";
            firstArticle.ArticlePublicationDate = DateTime.UtcNow;
            firstArticle.ArticleContent = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum";
            repository.AddArticle(firstArticle);

            Article secondArticle = new Article();
            secondArticle.ArticleName = "Second Article of this Blog";
            secondArticle.ArticlePublicationDate = DateTime.UtcNow;
            secondArticle.ArticleContent = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum";
            repository.AddArticle(secondArticle);

            Feedback firstFeedback = new Feedback();
            firstFeedback.FeedbackAuthor = "good_user_5151";
            firstFeedback.FeedbackPublicationDate = DateTime.UtcNow;
            firstFeedback.FeedbackContent = "damn, this website is pretty good!";
            repository.AddFeedback(firstFeedback);

            Feedback secondFeedback = new Feedback();
            secondFeedback.FeedbackAuthor = "bad_user_666";
            secondFeedback.FeedbackPublicationDate = DateTime.UtcNow;
            secondFeedback.FeedbackContent = "what a bullsh!t, the author of this garbage is a moron!";
            repository.AddFeedback(secondFeedback);
        }
    }
}