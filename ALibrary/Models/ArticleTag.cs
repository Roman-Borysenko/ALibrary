using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALibrary.Models
{
    public class ArticleTag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public List<Article> Articles { get; set; }
    }
}