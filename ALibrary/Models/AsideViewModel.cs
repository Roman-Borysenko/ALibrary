using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALibrary.Models
{
    public class AsideViewModel
    {
        public List<Banner> Advertising { get; set; }
        public List<Book> Books { get; set; }
        public List<ArticleTag> Tags { get; set; }
        public List<Article> Articles { get; set; }
    }
}