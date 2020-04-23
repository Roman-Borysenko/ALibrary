using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ALibrary.Models
{
    [Table("Advertising")]
    public class Banner
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public bool Place { get; set; }
        public DateTime Create { get; set; }
    }
}