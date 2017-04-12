using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using epam_asp_task.Models;
using epam_asp_task.Infrastructure;

namespace epam_asp_task.Controllers
{
    public class AdminController : Controller
    {
        BusinessLogic bl = new BusinessLogic();

        public ActionResult Index()
        {
            return View(bl.Articles.ToList());
        }

        public ActionResult EditArticle(int id)
        {
            Article article = bl.Articles.First(p => p.Id == id);
            return View(article);
        }

        public PartialViewResult PopularTags()
        {
            return PartialView(bl.GetPopularTags());
        }

        [HttpPost]
        public ActionResult EditArticle(Article article)
        {
            if (!ModelState.IsValid)
            {
                return View(article);
               
            }

            bl.SaveArticle(article);
            TempData["success"] = string.Format("Article {0} is saved", article.Name);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteArticle(int id)
        {
            Article article = bl.RemoveArticle(id);
            if (article != null)
            {
                TempData["message"] = string.Format("Article {0} is deleted", article.Name);
            }

            return RedirectToAction("Index");
        }

        public ActionResult CreateArticle()
        {
            ViewBag.New = true;
            return View("EditArticle", new Article());
        }
    }
}