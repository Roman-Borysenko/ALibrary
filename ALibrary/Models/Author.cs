using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALibrary.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Create { get; set; }
        public Country Country { get; set; }
        public List<Book> Books { get; set; }
    }
}