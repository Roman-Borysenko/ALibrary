using ALibrary.Helpers;
using ALibrary.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ALibrary.Controllers
{
    public class CategoryController : Controller
    {
        private const int pageSize = 3;
        public ActionResult Index(string category, int? page)
        {
            var pageNumber = (page ?? 1);
            var books = new List<Book>();
            var categoryViewModel = new CategoryViewModel();

            using (var context  = new DataContext())
            {
                var cat = context.Categories.FirstOrDefault(c => c.Slug == category);
                if(cat == null)
                {
                    return HttpNotFound(); 
                }

                foreach (var subcategory in cat.Subcategories)
                {
                    books.AddRange(context.Books.Include("Categories").Include("Author").Include("Author.Country")
                        .Where(b => b.Categories.Any(c => c.Id == subcategory.Id)).OrderByDescending(b => b.Create));
                }

                categoryViewModel.CategoryName = cat.Name;
                categoryViewModel.PageType = PageType.Category;
                categoryViewModel.Books = books.ToPagedList(pageNumber, pageSize);
            }

            return View("Category", categoryViewModel);
        }
        public ActionResult Subcategory(string category, string subcategory, int? page)
        {
            var pageNumber = (page ?? 1);
            var categoryViewModel = new CategoryViewModel();

            using (var context = new DataContext())
            {
                var categ = context.Categories.FirstOrDefault(c => c.Slug == category);
                var subcateg = context.Categories.Include("Books").Include("Books.Author")
                    .Include("Books.Author.Country").FirstOrDefault(c => c.Slug == subcategory);
                if(categ == null || subcateg == null || !categ.Subcategories.Any(s => s.Id == subcateg.Id))
                {
                    return HttpNotFound();
                }

                categoryViewModel.PageType = PageType.Subcategory;
                categoryViewModel.CategoryName = subcateg.Name;
                categoryViewModel.Books = subcateg.Books.ToPagedList(pageNumber, pageSize);
            }

            return View("Category", categoryViewModel);
        }
        public ActionResult Search(string search, string fieldSearch, int? page)
        {
            var pageNumber = (page ?? 1);
            var categoryViewModel = new CategoryViewModel();
            using (var context = new DataContext())
            {
                switch (Convert.ToInt32(fieldSearch))
                {
                    case (int)FieldsSearch.ByTitle: 
                        categoryViewModel.Books = context.Books.Include("Categories").Include("Author")
                            .Include("Author.Country").Where(b => b.Name.Contains(search))
                            .ToList().ToPagedList(pageNumber, pageSize); 
                        break;
                    case (int)FieldsSearch.ByYear:
                        if (int.TryParse(search, out int s))
                        {
                            categoryViewModel.Books = categoryViewModel.Books = context.Books.Include("Categories")
                            .Include("Author").Include("Author.Country").Where(b => b.Year == s)
                            .ToList().ToPagedList(pageNumber, pageSize);
                        }
                        break;
                    case (int)FieldsSearch.ByAuthor: 
                        categoryViewModel.Books = context.Books.Include("Categories").Include("Author")
                            .Include("Author.Country").Where(b => b.Author.Name.Contains(search))
                            .ToList().ToPagedList(pageNumber, pageSize);
                        break;
                    case (int)FieldsSearch.ByCountry:
                        categoryViewModel.Books = context.Books.Include("Categories").Include("Author")
                            .Include("Author.Country").Where(b => b.Author.Country.Name.Contains(search))
                            .ToList().ToPagedList(pageNumber, pageSize);  
                        break;
                }
            }

            categoryViewModel.CategoryName = "Результаты поиска: " + search;
            categoryViewModel.PageType = PageType.Search;

            var query = new Dictionary<string, string>();
            query.Add("search", search);
            query.Add("fieldSearch", fieldSearch);
            categoryViewModel.Query = query;

            return View("Category", categoryViewModel);
        }
    }
}