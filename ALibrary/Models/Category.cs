using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ALibrary.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public int? ParentId { get; set; }
        public DateTime Create { get; set; }
        public List<Book> Books { get; set; }
        [NotMapped]
        public List<Category> Subcategories 
        { 
            get 
            {
                using (var context  = new DataContext())
                {
                    return context.Categories.Where(c => c.ParentId == this.Id).ToList();
                }
            } 
        }
    }
}