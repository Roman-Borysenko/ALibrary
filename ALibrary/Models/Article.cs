using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALibrary.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public DateTime Create { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public int View { get; set; }
        public List<ArticleImages> ArticleImages { get; set; }
        public List<ArticleTag> ArticleTags { get; set; }
        public List<SimilarArticle> SimilarArticles { get; set; }
    }
}