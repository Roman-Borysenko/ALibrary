using ALibrary.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ALibrary.Models
{
    public class UpdateBook
    {
        public int Id { get; set; }
        [Required, MinLength(4), MaxLength(50)]
        public string Name { get; set; }
        [Required, Range(1000, 2020)]
        public int Year { get; set; }
        [Required, Range(50, 5000)]
        public int NumberPages { get; set; }
        [Required]
        public string Description { get; set; }
        [Range(0, 5)]
        public int TodayBestChoice { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [FileFormat(".pdf")]
        public HttpPostedFileBase BookFile { get; set; }
        [FileFormat(".jpg|.jpeg|.png")]
        public HttpPostedFileBase BookCover { get; set; }
        public string ImageName { get; set; }
    }
}