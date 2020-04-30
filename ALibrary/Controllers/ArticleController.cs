using ALibrary.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ALibrary.Controllers
{
    public class ArticleController : Controller
    {
        private const int pageSize = 3;
        public ActionResult Articles(int? page)
        {
            var pageNumber = (page ?? 1);
            using (var context = new DataContext())
            {
                var articles = context.Articles.Include("ArticleImages").Include("ArticleTags").ToList().ToPagedList(pageNumber, pageSize);
                return View(articles);
            }
        }
        public ActionResult Article(string slug)
        {
            return View();
        }
    }
}