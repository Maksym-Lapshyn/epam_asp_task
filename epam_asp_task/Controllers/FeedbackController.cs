using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using epam_asp_task.Infrastructure;
using epam_asp_task.Models;

namespace epam_asp_task.Controllers
{
    public class FeedbackController : Controller
    {
        ProjectRepository repository = new ProjectRepository();

        public ActionResult Feedback()
        {
            List<Feedback> feedbacks = repository.Feedbacks.ToList();
            return View(feedbacks);
        }

        public PartialViewResult FeedbackForm()
        {
            return PartialView(new Feedback());
        }

        [HttpPost]
        public ActionResult FeedbackForm(Feedback newFeedback)
        {
            if (ModelState.IsValid)
            {
                newFeedback.PublicationDate = DateTime.UtcNow;
                TempData["Message"] = "Hurray! Your feedback is added!";
                repository.AddFeedback(newFeedback);
                return RedirectToAction("Feedback");
            }
            return View("FeedbackForm");
        }

    }
}