using ALibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ALibrary.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        //[Authorize(Roles = "Admin")]
        public ActionResult Index(string category, string subcategory, int id, string slug)
        {
            var bookViewModel = new BookViewModel();
            using (var context = new DataContext())
            {
                var book = context.Books.Include("Categories").Include("Author").Include("Author.Country").FirstOrDefault(b => b.Id == id && b.Slug == slug && b.Categories.Any(s => s.Slug == subcategory && s.ParentId == context.Categories.FirstOrDefault(c => c.Slug == category).Id));
                if(book == null)
                {
                    return HttpNotFound();
                }

                bookViewModel.Book = book;
            }

            return View("Book", bookViewModel);
        }
    }
}