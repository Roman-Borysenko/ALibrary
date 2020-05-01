using ALibrary.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace ALibrary.Models
{
    public class AddArticleViewModel
    {
        [Required, StringLength(120, MinimumLength = 2)]
        public string Title { get; set; }
        [Required]
        public DateTime Create { get; set; }
        [Required, StringLength(120, MinimumLength = 10)]
        public string Description { get; set; }
        [Required, MinLength(30), AllowHtml]
        public string Text { get; set; }
        [Required, ListFileForrmat(".jpg|.jpeg|.png")]
        public IEnumerable<HttpPostedFileBase> Images { get; set; }
        public int[] SimilarArticles { get; set; }
        public int[] ArticleTags { get; set; }
    }
}