using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ALibrary.Models
{
    public class ArticleTag
    {
        public int Id { get; set; }
        [Required, StringLength(32, MinimumLength = 2)]
        public string Name { get; set; }
        public string Slug { get; set; }
        public List<Article> Articles { get; set; }
    }
}