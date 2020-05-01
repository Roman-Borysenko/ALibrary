﻿using ALibrary.Helpers;
using ALibrary.Models;
using PagedList;
using System.Linq;
using System.Web.Mvc;

namespace ALibrary.Controllers
{
    public class ArticleController : Controller
    {
        private const int pageSize = 5;
        public ActionResult Articles(int? page)
        {
            var pageNumber = (page ?? 1);
            var articlesViewModel = new ArticlesViewModel();
            using (var context = new DataContext())
            {
                articlesViewModel.Articles = context.Articles.Include("ArticleImages").Include("ArticleTags").OrderByDescending(a => a.Create).ToList().ToPagedList(pageNumber, pageSize);
            }

            articlesViewModel.Title = "Статьи";
            articlesViewModel.PageType = PageType.Articles;

            return View(articlesViewModel);
        }
        public ActionResult Article(string slug)
        {
            var articleViewModel = new ArticleViewModel();

            using (var context = new DataContext())
            {
                articleViewModel.Article = context.Articles.Include("ArticleImages").Include("ArticleTags").FirstOrDefault(a => a.Slug == slug);
            }

            return View(articleViewModel);
        }
        public ActionResult TagArticles(string tag, int? page)
        {
            var pageNumber = (page ?? 1);
            var articlesViewModel = new ArticlesViewModel();
            using (var context = new DataContext())
            {
                articlesViewModel.Articles = context.Articles.Include("ArticleImages").Include("ArticleTags").Where(a => a.ArticleTags.Any(t => t.Slug == tag)).OrderByDescending(a => a.Create).ToList().ToPagedList(pageNumber, pageSize);
                articlesViewModel.Title = "Статьи по тегу: " + context.ArticleTags.FirstOrDefault(t => t.Slug == tag).Name;
            }

            articlesViewModel.PageType = PageType.ArticlesTag;

            return View("Articles", articlesViewModel);
        }
    }
}