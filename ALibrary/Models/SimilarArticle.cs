using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALibrary.Models
{
    public class SimilarArticle
    {
        public int Id { get; set; }
        public Article Article { get; set; }
        public int SimilarArticleId { get; set; }
    }
}