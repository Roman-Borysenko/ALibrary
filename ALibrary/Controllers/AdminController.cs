using ALibrary.Models;
using SlugGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
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

        private void GetAuthorAndCategories()
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
            GetAuthorAndCategories();
            return View();
        }
        [HttpPost]
        public ActionResult AddBook(AddBook book)
        {
            if (!ModelState.IsValid)
            {
                GetAuthorAndCategories();
                return View(book);
            }

            using (var context = new DataContext())
            {
                string bookCover = Guid.NewGuid().ToString().Substring(0, 10) + "_" + System.IO.Path.GetFileName(book.BookCover.FileName);
                WebImage img = new WebImage(book.BookCover.InputStream);
                img.Resize(380, 600);
                img.Save(Server.MapPath("~/Content/images/book/" + bookCover));

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
                    Rating = book.Rating,
                    ForAuthorize = book.ForAuthorize,
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
            GetAuthorAndCategories();

            using (var context = new DataContext())
            {
                var book = context.Books.Include("Author").Include("Categories").Include("Author.Country").FirstOrDefault(b => b.Id == id);

                var updateBook = new UpdateBook
                {
                    Id = book.Id,
                    Name = book.Name,
                    Year = book.Year,
                    NumberPages = book.NumberPages,
                    Description = book.Description,
                    TodayBestChoice = book.TodayBestChoice,
                    Rating = book.Rating,
                    ForAuthorize = book.ForAuthorize,
                    AuthorId = book.Author.Id,
                    CategoryId = book.Categories.FirstOrDefault().Id,
                    ImageName = book.Image
                };

                return View(updateBook);
            }
        }
        [HttpPost]
        public ActionResult UpdateBook(UpdateBook book)
        {
            if (!ModelState.IsValid)
            {
                GetAuthorAndCategories();
                return View(book);
            }

            using (var context = new DataContext())
            {
                var updateBook = context.Books.Include("Categories").Include("Author").Include("Author.Country").FirstOrDefault(b => b.Id == book.Id);

                if (book.BookCover != null)
                {
                    string bookCover = Guid.NewGuid().ToString().Substring(0, 10) + "_" + System.IO.Path.GetFileName(book.BookCover.FileName);
                    WebImage img = new WebImage(book.BookCover.InputStream);
                    img.Resize(380, 600);
                    img.Save(Server.MapPath("~/Content/images/book/" + bookCover));
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
                updateBook.Author = context.Authors.FirstOrDefault(a => a.Id == book.AuthorId);
                updateBook.Description = book.Description;
                updateBook.View = 0;
                updateBook.Rating = book.Rating;
                updateBook.ForAuthorize = book.ForAuthorize;
                updateBook.TodayBestChoice = book.TodayBestChoice;
                updateBook.Create = DateTime.Now;
                updateBook.Categories = context.Categories.Where(c => c.Id == book.CategoryId).ToList();

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
                GetAuthorAndCategories();
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

        #region Article

        public void GetArticlesAndTags()
        {
            using (var context = new DataContext())
            {
                SelectList articles = new SelectList(context.Articles.ToList(), "Id", "Title");
                ViewBag.Articles = articles;

                SelectList tags = new SelectList(context.ArticleTags.ToList(), "Id", "Name");
                ViewBag.Tags = tags;
            }
        }

        public ActionResult ShowArticles()
        {
            using (var context = new DataContext())
            {
                return View(context.Articles.ToList());
            }
        }
        public ActionResult AddArticle()
        {
            GetArticlesAndTags();
            return View();
        }
        [HttpPost]
        public ActionResult AddArticle(AddArticleViewModel article)
        {
            if (!ModelState.IsValid)
            {
                GetArticlesAndTags();
                return View(article);
            }

            var articleImages = new List<ArticleImages>();
            foreach (var image in article.Images)
            {
                if (image != null)
                {
                    string imagePath = Guid.NewGuid().ToString().Substring(0, 10) + "_" + System.IO.Path.GetFileName(image.FileName);
                    articleImages.Add(new ArticleImages { ImagePath = imagePath });
                    WebImage img = new WebImage(image.InputStream);
                    img.Resize(1600, 600);
                    img.Save(Server.MapPath("~/Content/images/articles/" + imagePath));
                }
            }

            using (var context = new DataContext())
            {
                var articleSave = new Article
                {
                    Title = article.Title,
                    Slug = article.Title.GenerateSlug(),
                    Description = article.Description,
                    Text = article.Text,
                    Create = DateTime.Now,
                    ArticleImages = articleImages,
                    ArticleTags = article.ArticleTags != null ? context.ArticleTags.Where(a => article.ArticleTags.Contains(a.Id)).ToList() : null
                };

                if (article.SimilarArticles != null && article.SimilarArticles.Count() > 0)
                {
                    foreach (var similar in article.SimilarArticles)
                    {
                        context.SimilarArticles.Add(new SimilarArticle { Article = articleSave, SimilarArticleId = similar });
                    }
                }

                context.Articles.Add(articleSave);
                context.SaveChanges();
            }

            return Redirect("/admin/articles");
        }

        public ActionResult UpdateArticle(int id)
        {
            GetArticlesAndTags();

            using (var context = new DataContext())
            {
                var article = context.Articles.Include("ArticleImages").Include("ArticleTags").Include("SimilarArticles").FirstOrDefault(a => a.Id == id);

                var articleViewModel = new UpdateArticleViewModel
                {
                    Id = article.Id,
                    Title = article.Title,
                    Create = article.Create,
                    Description = article.Description,
                    Text = article.Text,
                    ArticleImages = article.ArticleImages,
                    SimilarArticles = article.SimilarArticles.Select(s => s.SimilarArticleId).ToArray(),
                    ArticleTags = article.ArticleTags.Select(t => t.Id).ToArray()
                };

                return View(articleViewModel);
            }
        }
        [HttpPost]
        public ActionResult UpdateArticle(UpdateArticleViewModel article)
        {
            if (!ModelState.IsValid)
            {
                GetArticlesAndTags();
                return View(article);
            }

            var articleImages = new List<ArticleImages>();
            foreach (var image in article.Images)
            {
                if (image != null)
                {
                    string imagePath = Guid.NewGuid().ToString().Substring(0, 10) + "_" + System.IO.Path.GetFileName(image.FileName);
                    articleImages.Add(new ArticleImages { ImagePath = imagePath });
                    WebImage img = new WebImage(image.InputStream);
                    img.Resize(1600, 600);
                    img.Save(Server.MapPath("~/Content/images/articles/" + imagePath));
                }
            }

            using (var context = new DataContext())
            {
                var articleUpdate = context.Articles.Include("ArticleImages").Include("ArticleTags").Include("SimilarArticles").FirstOrDefault(a => a.Id == article.Id);

                articleUpdate.Title = article.Title;
                articleUpdate.Slug = article.Title.GenerateSlug();
                articleUpdate.Description = article.Description;
                articleUpdate.Text = article.Text;
                //if(articleImages != null && articleImages.Count > 0)
                    articleUpdate.ArticleImages.AddRange(articleImages);
                articleUpdate.ArticleTags = context.ArticleTags.Where(t => article.ArticleTags.Contains(t.Id)).ToList();

                if (article.SimilarArticles != null && article.SimilarArticles.Count() > 0)
                {
                    context.SimilarArticles.RemoveRange(articleUpdate.SimilarArticles);

                    foreach (var similar in article.SimilarArticles)
                    {
                        context.SimilarArticles.Add(new SimilarArticle { Article = articleUpdate, SimilarArticleId = similar });
                    }
                }

                context.SaveChanges();
            }

            return Redirect("/admin/articles");
        }

        public ActionResult DeleteArticleImage(int image)
        {
            using (var context = new DataContext())
            {
                context.ArticleImages.Remove(context.ArticleImages.FirstOrDefault(i => i.Id == image));
                context.SaveChanges();
            }

            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }

        public void DeleteArticle(int id)
        {
            Response.Write(id);
        }

        #endregion
    }
}