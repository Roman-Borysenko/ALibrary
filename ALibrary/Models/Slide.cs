using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALibrary.Models
{
    public class Slide
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Signature { get; set; }
        public string Description { get; set; }
        public string LinkText { get; set; }
        public string Link { get; set; }
        public string Image { get; set; }
        public DateTime Create { get; set; }
    }
}