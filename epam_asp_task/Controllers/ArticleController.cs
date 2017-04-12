using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using epam_asp_task.Infrastructure;
using epam_asp_task.Models;

namespace epam_asp_task.Controllers
{
    public class ArticleController : Controller
    {
        BusinessLogic bl = new BusinessLogic();

        public ActionResult Article(int articleId)
        {
            Article article = bl.Articles.Where(a => a.Id == articleId).First();
            return View(article);
        }

        public PartialViewResult SimilarArticles(int articleId)
        {
            Dictionary<int, string> similarArticles = bl.GetSimilarArticles(articleId);
            return PartialView(similarArticles);
        }
    }
}