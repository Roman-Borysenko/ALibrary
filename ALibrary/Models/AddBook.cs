using ALibrary.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ALibrary.Models
{
    public class AddBook
    {
        [Required, MinLength(4), MaxLength(50)]
        public string Name { get; set; }
        [Required, Range(1000, 2020)]
        public int Year { get; set; }
        [Required, Range(50, 5000)]
        public int NumberPages { get; set; }
        [Required, AllowHtml]
        public string Description { get; set; }
        [Range(0, 5)]
        public int TodayBestChoice { get; set; }
        [Required, Range(0, 5)]
        public int Rating { get; set; }
        [Required]
        public bool ForAuthorize { get; set; }
        [Required]
        public int Author { get; set; }
        [Required]
        public int Category { get; set; }
        [Required, FileFormat(".pdf")]
        public HttpPostedFileBase BookFile { get; set; }
        [Required, FileFormat(".jpg|.jpeg|.png")]
        public HttpPostedFileBase BookCover { get; set; }
    }
}