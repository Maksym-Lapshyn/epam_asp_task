using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using epam_asp_task.Infrastructure;
using epam_asp_task.Models;

namespace epam_asp_task.Controllers
{
    public class CommentController : Controller
    {
        ProjectRepository repository = new ProjectRepository();

        public PartialViewResult AddComment()
        {
            return PartialView(new Comment());
        }

        [HttpPost]
        public ActionResult AddComment(Comment newComment, int parentId, bool article)
        {
            if (ModelState.IsValid)
            {
                newComment.PublicationDate = DateTime.UtcNow;
                TempData["Message"] = "Hurray! Your comment is added!";
                if (article)
                {
                    repository.AddCommentForArticle(newComment, parentId);
                }
                else
                {
                    repository.AddCommentForComment(newComment, parentId);
                }
                return RedirectToAction("Article", "Article");
            }
            return View("AddComment");
        }
    }
}