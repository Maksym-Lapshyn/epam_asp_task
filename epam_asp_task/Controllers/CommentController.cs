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
        BusinessLogic bl = new BusinessLogic();

        [HttpGet]
        public PartialViewResult CommentForm(int parentId, bool parentIsArticle)
        {
            Comment comment = new Comment();
            comment.ParentId = parentId;
            comment.ParentIsArticle = parentIsArticle;
            return PartialView(comment);
        }

        [HttpPost]
        public ActionResult CommentForm(Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return View(comment);
            }

            bl.AddComment(comment);
            TempData["Message"] = "Hurray! Your comment is added!";
            
            return RedirectToAction("Article", "Article", new { articleId = Convert.ToInt32(Session["ArticleId"]) });
        }
    }
}