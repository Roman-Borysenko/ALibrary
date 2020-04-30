using ALibrary.Helpers;
using PagedList;

namespace ALibrary.Models
{
    public class ArticlesViewModel
    {
        public string Title { get; set; }
        public IPagedList<Article> Articles { get; set; }
        public PageType PageType { get; set; }
    }
}