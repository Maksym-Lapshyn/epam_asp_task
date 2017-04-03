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
        ProjectRepository repository = new ProjectRepository();

        public ActionResult Article(int articleId)
        {
            Article article = repository.Articles.Where(a => a.Id == articleId).First();
            return View(article);
        }
    }
}