using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALibrary.Models
{
    public class ArticleImages
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public Article Article { get; set; }
    }
}