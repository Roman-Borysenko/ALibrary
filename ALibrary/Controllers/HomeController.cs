using ALibrary.Models;
using System.Linq;
using System.Web.Mvc;

namespace ALibrary.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var homeViewModel = new HomeViewModel();
            using (var context = new DataContext())
            {
                homeViewModel.TodayBestChoice = context.Books.Include("Categories").Where(b => b.TodayBestChoice != 0).OrderByDescending(b=>b.TodayBestChoice).ThenByDescending(b => b.Rating).Take(6).ToList();
                homeViewModel.NewBooks = context.Books.Include("Categories").OrderByDescending(b => b.Create).Take(8).ToList();
                homeViewModel.Slider = context.Slider.Take(5).OrderByDescending(s => s.Create).ToList();
                homeViewModel.Articles = context.Articles.Include("ArticleImages").OrderByDescending(a => a.Create).Take(3).ToList();
            }
            return View(homeViewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}