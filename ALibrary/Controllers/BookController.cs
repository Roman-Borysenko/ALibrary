using ALibrary.Models;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ALibrary.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index(string category, string subcategory, int id, string slug)
        {
            var bookViewModel = new BookViewModel();
            using (var context = new DataContext())
            {
                var book = context.Books.Include("Comments").Include("Categories").Include("Author").Include("Author.Country").FirstOrDefault(b => b.Id == id && b.Slug == slug && b.Categories.Any(s => s.Slug == subcategory && s.ParentId == context.Categories.FirstOrDefault(c => c.Slug == category).Id));
                if(book == null)
                {
                    return HttpNotFound();
                }

                book.View += 1;

                bookViewModel.Book = book;

                context.SaveChanges();
            }

            return View("Book", bookViewModel);
        }

        public ActionResult ReadBook(int id, string slug)
        {
            using (var context = new DataContext())
            {
                var book = context.Books.Include("Categories").Include("Author").Include("Author.Country").FirstOrDefault(b => b.Id == id && b.Slug == slug);

                if (book == null)
                    return HttpNotFound();
                if ((User.Identity.IsAuthenticated != book.ForAuthorize && book.ForAuthorize != false))
                    return HttpNotFound();

                book.View += 1;

                context.SaveChanges();

                return View(book);
            }
        }
        [Authorize]
        public ActionResult Watchlist(int? page)
        {
            var pageSize = 10;
            var pageNumber = (page ?? 1);
            var categoryViewModel = new CategoryViewModel();

            using (var context = new DataContext())
            {
                string userId = User.Identity.GetUserId();

                var listBooksId = context.Watchlists.Where(w => w.UserId == userId).Select(w => w.BookId);
                categoryViewModel.Books = context.Books.Include("Categories").Include("Author").Include("Author.Country").Where(b => listBooksId.Contains(b.Id)).ToList().ToPagedList(pageNumber, pageSize); ;
            }

            categoryViewModel.CategoryName = "Список наблюдения";
            categoryViewModel.PageType = Helpers.PageType.Watchlist;

            return View("../Category/Category", categoryViewModel);
        }
        [HttpPost, Authorize]
        public ActionResult Watchlist(int id, string slug)
        {
            using (var context = new DataContext())
            {
                var book = context.Books.FirstOrDefault(b => b.Id == id && b.Slug == slug);
                if (book != null)
                {
                    string userId = User.Identity.GetUserId();
                    if (context.Watchlists.FirstOrDefault(w => w.BookId == id && w.UserId == userId) == null)
                    {
                        context.Watchlists.Add(new Watchlist { BookId = id, UserId = userId });
                        context.SaveChanges();
                    }
                    return Redirect(Request.UrlReferrer.AbsoluteUri);
                } else
                {
                    return HttpNotFound();
                }
            }
        }
        [Authorize, HttpPost]
        public ActionResult AddComment(int book, string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                using (var context = new DataContext())
                {
                    var comment = new Comment
                    {
                        Text = text,
                        UserId = User.Identity.GetUserId(),
                        Book = context.Books.FirstOrDefault(b => b.Id == book),
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