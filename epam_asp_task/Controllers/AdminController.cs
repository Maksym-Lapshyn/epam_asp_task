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
        ProjectRepository repository = new ProjectRepository();

        public ActionResult Index()
        {
            return View(repository.Articles.ToList());
        }

        public ActionResult EditArticle(int id)
        {
            Article article = repository.Articles.First(p => p.Id == id);
            return View(article);
        }

        public PartialViewResult PopularTags()
        {
            return PartialView(repository.GetPopularTags());
        }

        [HttpPost]
        public ActionResult EditArticle(Article article)
        {
            if (ModelState.IsValid)
            {
                repository.SaveArticle(article);
                TempData["success"] = string.Format("Article {0} is saved", article.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(article);
            }
        }

        [HttpPost]
        public ActionResult DeleteArticle(int id)
        {
            Article article = repository.RemoveArticle(id);
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