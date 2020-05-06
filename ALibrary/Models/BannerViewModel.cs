using ALibrary.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ALibrary.Models
{
    public class BannerViewModel
    {
        public int Id { get; set; }
        [Required, StringLength(120, MinimumLength = 2)]
        public string Text { get; set; }
        public string ImagePath { get; set; }
        [Required]
        public bool Place { get; set; }
        [FileFormat(".jpg|.jpeg|.png")]
        public HttpPostedFileBase Image { get; set; }
    }
}