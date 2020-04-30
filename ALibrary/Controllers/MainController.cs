using ALibrary.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ALibrary.Controllers
{
    public class MainController : Controller
    {
        public ActionResult Menu()
        {
            using (var context = new DataContext())
            {
                var categories = context.Categories.Where(c => c.ParentId == null).ToList();
                return PartialView(categories);
            }
        }

        public ActionResult Aside()
        {
            var asideViewModel = new AsideViewModel();
            using (var context = new DataContext())
            {
                asideViewModel.Advertising = context.Advertising.Where(a => a.Place == true).OrderByDescending(a => a.Create).Take(3).ToList();
                asideViewModel.Books = context.Books.Include("Categories").OrderByDescending(b => b.View).ThenByDescending(b => b.Create).Take(8).ToList();
                asideViewModel.Tags = context.ArticleTags.ToList();
                asideViewModel.Articles = context.Articles.OrderByDescending(a => a.View).Take(5).ToList();
            }
            return PartialView(asideViewModel);
        }

        public ActionResult TopBanner()
        {
            var topBanner = new Banner();
            using (var context = new DataContext())
            {
                topBanner = context.Advertising.OrderByDescending(a => a.Create).FirstOrDefault(a => a.Place == false);
            }
            return PartialView(topBanner);
        }
    }
}