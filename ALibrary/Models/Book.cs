using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALibrary.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public int Year { get; set; }
        public int NumberPages { get; set; }
        public Author Author { get; set; }
        public string Description { get; set; }
        public string BookPath { get; set; }
        public string Image { get; set; }
        public int View { get; set; }
        public int Rating { get; set; }
        public int TodayBestChoice { get; set; }
        public DateTime Create { get; set; }
        public List<Category> Categories { get; set; }
        public List<Comment> Comments { get; set; }
    }
}