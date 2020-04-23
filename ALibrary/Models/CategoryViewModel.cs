using ALibrary.Helpers;
using PagedList;
using System.Collections.Generic;

namespace ALibrary.Models
{
    public class CategoryViewModel
    {
        public string CategoryName { get; set; }
        public IPagedList<Book> Books { get; set; }
        public PageType PageType { get; set; }
        public Dictionary<string, string> Query { get; set; }
    }
}