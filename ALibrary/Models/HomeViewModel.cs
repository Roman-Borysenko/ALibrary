using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALibrary.Models
{
    public class HomeViewModel
    {
        public List<Book> TodayBestChoice { get; set; }
        public List<Book> NewBooks { get; set; }
        public List<Slide> Slider { get; set; }
    }
}