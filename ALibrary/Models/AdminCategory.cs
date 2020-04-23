using System.ComponentModel.DataAnnotations;

namespace ALibrary.Models
{
    public class AdminCategory
    {
        public int Id { get; set; }
        [Required, MinLength(4), MaxLength(30)]
        public string CategoryName { get; set; }
        public int? ParentCategory { get; set; }
        public bool IsEditPage { get; set; }
    }
}