using ALibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ALibrary.Controllers
{
    public class ArticleController : Controller
    {
        // GET: Article
        public ActionResult Articles()
        {
            using (var context = new DataContext())
            {
                var articles = context.Articles.Include("ArticleImages").Include("ArticleTags").ToList();
                return View(articles);
            }
        }
        public ActionResult Article(string slug)
        {
            return View();
        }
    }
}