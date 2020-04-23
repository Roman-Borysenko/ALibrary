using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALibrary.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Create { get; set; }
        public List<Author> Authors { get; set; }
    }
}