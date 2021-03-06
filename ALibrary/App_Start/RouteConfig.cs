﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ALibrary
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            #region Admin Routes

            routes.MapRoute(
                name: "Admin",
                url: "admin",
                defaults: new { controller = "Admin", action = "Index" }
            );

            #region Admin books

            routes.MapRoute(
                name: "ShowBook",
                url: "admin/books",
                defaults: new { controller = "Admin", action = "ShowBook" }
            );

            routes.MapRoute(
                name: "AddBook",
                url: "admin/book/add",
                defaults: new { controller = "Admin", action = "AddBook" }
            );

            routes.MapRoute(
                name: "UpdateBook",
                url: "admin/book/update/{id}",
                defaults: new { controller = "Admin", action = "UpdateBook", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "DeleteBook",
                url: "admin/book/delete/{id}",
                defaults: new { controller = "Admin", action = "DeleteBook" }
            );

            #endregion

            #region Admin Categories

            routes.MapRoute(
                name: "ShowCategories",
                url: "admin/categories",
                defaults: new { controller = "Admin", action = "ShowCategories" }
            );

            routes.MapRoute(
                name: "AddSubcategory",
                url: "admin/subcategory/add",
                defaults: new { controller = "Admin", action = "AddSubcategory" }
            );

            routes.MapRoute(
                name: "AddCategory",
                url: "admin/category/add",
                defaults: new { controller = "Admin", action = "AddCategory" }
            );

            routes.MapRoute(
                name: "UpdateCategory",
                url: "admin/category/update/{id}",
                defaults: new { controller = "Admin", action = "UpdateCategory" }
            );

            routes.MapRoute(
                name: "DeleteCategory",
                url: "admin/category/delete/{id}",
                defaults: new { controller = "Admin", action = "DeleteCategory" }
            );

            #endregion

            #region Admin Authors

            routes.MapRoute(
                name: "ShowAuthors",
                url: "admin/authors",
                defaults: new { controller = "Admin", action = "ShowAuthors" }
            );

            routes.MapRoute(
                name: "AddAuthor",
                url: "admin/author/add",
                defaults: new { controller = "Admin", action = "AddAuthor" }
            );

            routes.MapRoute(
                name: "UpdateAuthor",
                url: "admin/author/update/{id}",
                defaults: new { controller = "Admin", action = "UpdateAuthor" }
            );

            routes.MapRoute(
                name: "DeleteAuthor",
                url: "admin/author/delete/{id}",
                defaults: new { controller = "Admin", action = "DeleteAuthor" }
            );

            #endregion

            #region Countries

            routes.MapRoute(
                name: "ShowCountries",
                url: "admin/countries",
                defaults: new { controller = "Admin", action = "ShowCountries" }
            );

            routes.MapRoute(
                name: "AddCountry",
                url: "admin/country/add",
                defaults: new { controller = "Admin", action = "AddCountry" }
            );

            routes.MapRoute(
                name: "UpdateCountry",
                url: "admin/country/update/{id}",
                defaults: new { controller = "Admin", action = "UpdateCountry" }
            );

            routes.MapRoute(
                name: "DeleteCountry",
                url: "admin/country/delete/{id}",
                defaults: new { controller = "Admin", action = "DeleteCountry" }
            );

            #endregion

            #region Article

            routes.MapRoute(
                name: "ShowArticles",
                url: "admin/articles",
                defaults: new { controller = "Admin", action = "ShowArticles" }
            );

            routes.MapRoute(
                name: "AddArticle",
                url: "admin/article/add",
                defaults: new { controller = "Admin", action = "AddArticle" }
            );

            routes.MapRoute(
                name: "UpdateArticle",
                url: "admin/article/edit/{id}",
                defaults: new { controller = "Admin", action = "UpdateArticle" }
            );

            routes.MapRoute(
                name: "DeleteArticle",
                url: "admin/article/delete/{id}",
                defaults: new { controller = "Admin", action = "DeleteArticle" }
            );

            routes.MapRoute(
                name: "DeleteArticleImage",
                url: "admin/article/image/delete/{image}",
                defaults: new { controller = "Admin", action = "DeleteArticleImage" }
            );

            #endregion

            #region Tags

            routes.MapRoute(
                name: "ShowTags",
                url: "admin/tags",
                defaults: new { controller = "Admin", action = "ShowTags" }
            );

            routes.MapRoute(
                name: "AddTag",
                url: "admin/tag/add",
                defaults: new { controller = "Admin", action = "AddTag" }
            );

            routes.MapRoute(
                name: "EditTag",
                url: "admin/tag/edit/{slug}",
                defaults: new { controller = "Admin", action = "EditTag" }
            );

            routes.MapRoute(
                name: "DeleteTag",
                url: "admin/tag/delete/{slug}",
                defaults: new { controller = "Admin", action = "DeleteTag" }
            );

            #endregion

            #region Slider

            routes.MapRoute(
                name: "ShowSlides",
                url: "admin/slides",
                defaults: new { controller = "Admin", action = "ShowSlides" }
            );

            routes.MapRoute(
                name: "AddSlide",
                url: "admin/slide/add",
                defaults: new { controller = "Admin", action = "AddSlide" }
            );

            routes.MapRoute(
                name: "EditSlide",
                url: "admin/slide/edit/{id}",
                defaults: new { controller = "Admin", action = "EditSlide" }
            );

            routes.MapRoute(
                name: "DeleteSlide",
                url: "admin/slide/delete/{id}",
                defaults: new { controller = "Admin", action = "DeleteSlide" }
            );

            #endregion

            #region Banner

            routes.MapRoute(
                name: "ShowBanners",
                url: "admin/banners",
                defaults: new { controller = "Admin", action = "ShowBanners" }
            );

            routes.MapRoute(
                name: "AddBanner",
                url: "admin/banner/add",
                defaults: new { controller = "Admin", action = "AddBanner" }
            );

            routes.MapRoute(
                name: "EditBanner",
                url: "admin/banner/edit/{id}",
                defaults: new { controller = "Admin", action = "EditBanner" }
            );

            routes.MapRoute(
                name: "DeleteBanner",
                url: "admin/banner/delete/{id}",
                defaults: new { controller = "Admin", action = "DeleteBanner" }
            );

            #endregion

            #endregion

            #region Routes Site

            routes.MapRoute(
                name: "Articles",
                url: "articles",
                defaults: new { controller = "Article", action = "Articles" }
            );

            routes.MapRoute(
                name: "Article",
                url: "article/{slug}",
                defaults: new { controller = "Article", action = "Article" }
            );

            routes.MapRoute(
                name: "AddCommentForArticle",
                url: "article/comment/add",
                defaults: new { controller = "Article", action = "AddComment" }
            );

            routes.MapRoute(
                name: "TagArticles",
                url: "articles/tag/{tag}",
                defaults: new { controller = "Article", action = "TagArticles" }
            );

            routes.MapRoute(
                name: "TagArticlesPage",
                url: "articles/tag/{tag}/page/{page}",
                defaults: new { controller = "Article", action = "TagArticles" }
            );

            routes.MapRoute(
                name: "ArticlesPage",
                url: "articles/page/{page}",
                defaults: new { controller = "Article", action = "Articles" }
            );

            routes.MapRoute(
                name: "Watchlist",
                url: "watchlist",
                defaults: new { controller = "Book", action = "Watchlist" }
            );

            routes.MapRoute(
                name: "WatchlistPage",
                url: "watchlist/{page}",
                defaults: new { controller = "Book", action = "Watchlist" }
            );

            routes.MapRoute(
                name: "AddComment",
                url: "comment",
                defaults: new { controller = "Book", action = "AddComment" }
            );

            routes.MapRoute(
                name: "Register",
                url: "register",
                defaults: new { controller = "Account", action = "Register" }
            );

            routes.MapRoute(
                name: "LogOff",
                url: "logout",
                defaults: new { controller = "Account", action = "LogOff" }
            );

            routes.MapRoute(
                name: "Login",
                url: "login",
                defaults: new { controller = "Account", action = "Login" }
            );

            routes.MapRoute(
                name: "Search",
                url: "search",
                defaults: new { controller = "Category", action = "Search" }
            );

            routes.MapRoute(
                name: "Category",
                url: "{category}",
                defaults: new { controller = "Category", action = "Index" }
            );

            routes.MapRoute(
                name: "CategoryPage",
                url: "{category}/page/{page}",
                defaults: new { controller = "Category", action = "Index" }
            );

            routes.MapRoute(
                name: "Subcategory",
                url: "{category}/{subcategory}",
                defaults: new { controller = "Category", action = "Subcategory" }
            );

            routes.MapRoute(
                name: "SubcategoryPage",
                url: "{category}/{subcategory}/page/{page}",
                defaults: new { controller = "Category", action = "Subcategory" }
            );

            routes.MapRoute(
                name: "ReadBook",
                url: "read/{id}/{slug}",
                defaults: new { controller = "Book", action = "ReadBook" }
            );

            routes.MapRoute(
                name: "Book",
                url: "{category}/{subcategory}/{id}/{slug}",
                defaults: new { controller = "Book", action = "Index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            #endregion
        }
    }
}
