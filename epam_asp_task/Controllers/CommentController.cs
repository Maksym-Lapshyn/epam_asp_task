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

        public PartialViewResult CommentForm(int parentId, bool isArticle)
        {
            ViewBag.ParentId = parentId;
            ViewBag.IsArticle = isArticle;
            return PartialView(new Comment());
        }

        [HttpPost]
        public ActionResult PostComment(Comment newComment, int parentId, bool isArticle)
        {
            if (ModelState.IsValid)
            {
                newComment.PublicationDate = DateTime.UtcNow;
                TempData["Message"] = "Hurray! Your comment is added!";
                if (isArticle)
                {
                    repository.AddCommentForArticle(newComment, parentId);
                }
                else
                {
                    repository.AddCommentForComment(newComment, parentId);
                }
                return RedirectToAction("Article", "Article", new { articleId = Convert.ToInt32(Session["ArticleId"]) });
            }
            return View("CommentForm");
        }
    }
}