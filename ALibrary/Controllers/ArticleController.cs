﻿using ALibrary.Helpers;
using ALibrary.Models;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
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
                articleViewModel.Article = context.Articles.Include("ArticleImages").Include("ArticleTags").Include("SimilarArticles").Include("Comments").FirstOrDefault(a => a.Slug == slug);

                if (articleViewModel.Article == null)
                {
                    return HttpNotFound();
                }

                articleViewModel.Article.View += 1;

                var similarArticlesId = articleViewModel.Article.SimilarArticles.Select(s => s.SimilarArticleId);
                articleViewModel.SimilarArticles = context.Articles.Include("ArticleImages").Include("SimilarArticles").Where(a => similarArticlesId.Any(s => s == a.Id)).OrderByDescending(a => a.Create).Take(3).ToList();

                context.SaveChanges();
            }

            ViewBag.Article = true;

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
        [Authorize, HttpPost]
        public ActionResult AddComment(int article, string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                using (var context = new DataContext())
                {
                    var comment = new Comment
                    {
                        Text = text,
                        UserId = User.Identity.GetUserId(),
                        Article = context.Articles.FirstOrDefault(b => b.Id == article),
                        Create = DateTime.Now
                    };

                    context.Comments.Add(comment);
                    context.SaveChanges();
                }
            }
            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }
    }
}