using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALibrary.Models
{
    public class Watchlist
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int BookId { get; set; }
    }
}