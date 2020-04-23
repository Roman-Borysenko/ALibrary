﻿using ALibrary.Models;
using Newtonsoft.Json;
using SlugGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ALibrary.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        #region Book
        public ActionResult ShowBook()
        {
            var books = new List<Book>();
            using (var context = new DataContext())
            {
                books = context.Books.Include("Categories").Include("Author").Include("Author.Country").ToList();
            }
            return View(books);
        }

        private void GetAuthor()
        {
            using (var context = new DataContext())
            {
                SelectList authors = new SelectList(context.Authors.ToList(), "Id", "Name");
                SelectList categories = new SelectList(context.Categories.ToList(), "Id", "Name");
                ViewBag.Author = authors;
                ViewBag.Categories = categories;
            }
        }

        public ActionResult AddBook()
        {
            GetAuthor();
            return View();
        }
        [HttpPost]
        public ActionResult AddBook(AddBook book)
        {
            if (!ModelState.IsValid)
            {
                GetAuthor();
                return View(book);
            }

            using (var context = new DataContext())
            {
                string bookCover = Guid.NewGuid().ToString().Substring(0, 10) + "_" + System.IO.Path.GetFileName(book.BookCover.FileName);
                book.BookCover.SaveAs(Server.MapPath("~/Content/images/book/" + bookCover));

                string bookPath = Guid.NewGuid().ToString().Substring(0, 10) + "_" + System.IO.Path.GetFileName(book.BookFile.FileName);
                book.BookFile.SaveAs(Server.MapPath("~/Content/books/" + bookPath));

                var bookSave = new Book
                {
                    Name = book.Name,
                    Slug = book.Name.GenerateSlug(),
                    Year = book.Year,
                    NumberPages = book.NumberPages,
                    Author = context.Authors.FirstOrDefault(a => a.Id == book.Author),
                    Description = book.Description,
                    BookPath = bookPath,
                    Image = bookCover,
                    View = 0,
                    Rating = 3,
                    TodayBestChoice = book.TodayBestChoice,
                    Create = DateTime.Now,
                    Categories = context.Categories.Where(c => c.Id == book.Category).ToList()
                };

                context.Books.Add(bookSave);
                context.SaveChanges();
            }

            return Redirect("/admin/books");
        }

        public ActionResult UpdateBook(int id)
        {
            GetAuthor();

            using (var context = new DataContext())
            {
                var book = context.Books.Include("Categories").Include("Author").Include("Author.Country").FirstOrDefault(b => b.Id == id);

                var updateBook = new UpdateBook
                {
                    Id = book.Id,
                    Name = book.Name,
                    Year = book.Year,
                    NumberPages = book.NumberPages,
                    Description = book.Description,
                    TodayBestChoice = book.TodayBestChoice,
                    Author = book.Author.Id,
                    Category = book.Categories.FirstOrDefault().Id
                };

                return View(updateBook);
            }
        }
        [HttpPost]
        public ActionResult UpdateBook(UpdateBook book)
        {
            if (!ModelState.IsValid)
            {
                GetAuthor();
                return View(book);
            }

            using (var context = new DataContext())
            {
                var updateBook = context.Books.Include("Categories").Include("Author").Include("Author.Country").FirstOrDefault(b => b.Id == book.Id);

                if (book.BookCover != null)
                {
                    string bookCover = Guid.NewGuid().ToString().Substring(0, 10) + "_" + System.IO.Path.GetFileName(book.BookCover.FileName);
                    book.BookCover.SaveAs(Server.MapPath("~/Content/images/book/" + bookCover));
                    updateBook.Image = bookCover;
                }

                if (book.BookFile != null)
                {
                    string bookPath = Guid.NewGuid().ToString().Substring(0, 10) + "_" + System.IO.Path.GetFileName(book.BookFile.FileName);
                    book.BookFile.SaveAs(Server.MapPath("~/Content/books/" + bookPath));
                    updateBook.BookPath = bookPath;
                }

                updateBook.Name = book.Name;
                updateBook.Slug = book.Name.GenerateSlug();
                updateBook.Year = book.Year;
                updateBook.NumberPages = book.NumberPages;
                updateBook.Author = context.Authors.FirstOrDefault(a => a.Id == book.Author);
                updateBook.Description = book.Description;
                updateBook.View = 0;
                updateBook.Rating = 3;
                updateBook.TodayBestChoice = book.TodayBestChoice;
                updateBook.Create = DateTime.Now;
                updateBook.Categories = context.Categories.Where(c => c.Id == book.Category).ToList();

                context.SaveChanges();
            }

            return Redirect("/admin/books");
        }

        public ActionResult DeleteBook(int id)
        {
            using (var context = new DataContext())
            {
                context.Books.Remove(context.Books.FirstOrDefault(b => b.Id == id));
                context.SaveChanges();
            }

            return Redirect("/admin/books");
        }
        #endregion

        #region Category

        private void GetCategories()
        {
            using (var context = new DataContext())
            {
                SelectList categories = new SelectList(context.Categories.Where(c => c.ParentId == null).ToList(), "Id", "Name");
                ViewBag.Categories = categories;
            }
        }


        public ActionResult ShowCategories()
        {
            var categories = new List<Category>();
            using (var context = new DataContext())
            {
                categories = context.Categories.ToList();
            }
            return View(categories);
        }

        public ActionResult AddSubcategory()
        {
            GetCategories();
            var category = new AdminCategory
            {
                ParentCategory = 0
            };
            return View(category);
        }
        [HttpPost]
        public ActionResult AddSubcategory(AdminCategory category)
        {
            using (var context = new DataContext())
            {
                var categorySave = new Category
                {
                    Name = category.CategoryName,
                    Slug = category.CategoryName.GenerateSlug(),
                    ParentId = category.ParentCategory,
                    Create = DateTime.Now
                };

                context.Categories.Add(categorySave);
                context.SaveChanges();
            }

            return Redirect("/admin/categories");
        }

        public ActionResult AddCategory()
        {
            GetCategories();
            return View("AddSubcategory");
        }

        public ActionResult UpdateCategory(int id)
        {
            GetCategories();

            using (var context = new DataContext())
            {
                var updateCategory = context.Categories.FirstOrDefault(c => c.Id == id);
                var category = new AdminCategory
                {
                    Id = updateCategory.Id,
                    CategoryName = updateCategory.Name,
                    ParentCategory = updateCategory.ParentId,
                    IsEditPage = true
                };

                return View("AddSubcategory", category);
            }
        }
        [HttpPost]
        public ActionResult UpdateCategory(AdminCategory category)
        {
            if (!ModelState.IsValid)
            {
                GetAuthor();
                return View("AddSubcategory", category);
            }

            using (var context = new DataContext())
            {
                var categoryUpdate = context.Categories.FirstOrDefault(c => c.Id == category.Id);

                categoryUpdate.Name = category.CategoryName;
                categoryUpdate.Slug = category.CategoryName.GenerateSlug();
                categoryUpdate.ParentId = category.ParentCategory;

                context.SaveChanges();
            }

            return Redirect("/admin/categories");
        }
        public ActionResult DeleteCategory(int id)
        {
            using (var context = new DataContext())
            {
                context.Categories.Remove(context.Categories.FirstOrDefault(c => c.Id == id));
                context.SaveChanges();
            }
            return Redirect("/admin/categories");
        }
        #endregion

        #region Author

        public void GetCountries()
        {
            using (var context = new DataContext())
            {
                SelectList countries = new SelectList(context.Countries.ToList(), "Id", "Name");
                ViewBag.Countries = countries;
            }
        }

        public ActionResult ShowAuthors()
        {
            using (var context = new DataContext())
            {
                var authors = context.Authors.Include("Country").ToList();
                return View(authors);
            }
        }

        public ActionResult AddAuthor()
        {
            GetCountries();
            return View();
        }
        [HttpPost]
        public ActionResult AddAuthor(AdminAuthor author)
        {
            if (!ModelState.IsValid)
            {
                GetCountries();
                return View(author);
            }

            using (var context = new DataContext())
            {
                var authorSave = new Author
                {
                    Name = author.Name,
                    Country = context.Countries.FirstOrDefault(c => c.Id == author.CountryId),
                    Create = DateTime.Now
                };
                context.Authors.Add(authorSave);
                context.SaveChanges();
            }

            return Redirect("/admin/authors");
        }

        public ActionResult UpdateAuthor(int id)
        {
            GetCountries();

            using (var context = new DataContext())
            {
                var author = context.Authors.Include("Country").FirstOrDefault(a => a.Id == id);
                var updateAuthor = new AdminAuthor
                {
                    Name = author.Name,
                    CountryId = author.Country.Id,
                    IsEditPage = true
                };
                return View("AddAuthor", updateAuthor);
            }
        }
        [HttpPost]
        public ActionResult UpdateAuthor(AdminAuthor author)
        {
            if (!ModelState.IsValid)
            {
                GetCountries();
                return View(author);
            }

            using (var context = new DataContext())
            {
                var updateAuthor = context.Authors.Include("Country").FirstOrDefault(a => a.Id == author.Id);

                updateAuthor.Name = author.Name;
                updateAuthor.Country = context.Countries.FirstOrDefault(c => c.Id == author.CountryId);

                context.SaveChanges();
            }

            return Redirect("/admin/authors");
        }

        public ActionResult DeleteAuthor(int id)
        {
            using (var context = new DataContext())
            {
                context.Authors.Remove(context.Authors.FirstOrDefault(a => a.Id == id));
                context.SaveChanges();
            }
            return Redirect("/admin/authors");
        }

        #endregion

        #region Countries

        public ActionResult ShowCountries()
        {
            using (var context = new DataContext())
            {
                var countries = context.Countries.ToList();
                return View(countries);
            }
        }

        public ActionResult AddCountry()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCountry(Country country)
        {
            using(var context = new DataContext())
            {
                country.Create = DateTime.Now;
                context.Countries.Add(country);
                context.SaveChanges();
            }

            return Redirect("/admin/countries");
        }

        public ActionResult UpdateCountry(int id)
        {
            using (var context = new DataContext())
            {
                ViewBag.IsEditPage = true;
                var country = context.Countries.FirstOrDefault(c => c.Id == id);
                return View("AddCountry", country);
            }
        }
        [HttpPost]
        public ActionResult UpdateCountry(Country country)
        {
            using (var context = new DataContext())
            {
                var updateCountry = context.Countries.FirstOrDefault(c => c.Id == country.Id);
                updateCountry.Name = country.Name;

                context.SaveChanges();
            }

            return Redirect("/admin/countries");
        }

        public ActionResult DeleteCountry(int id)
        {
            using (var context = new DataContext())
            {
                context.Countries.Remove(context.Countries.FirstOrDefault(c => c.Id == id));
                context.SaveChanges();
            }

            return Redirect("/admin/countries");
        }

        #endregion
    }
}