using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ALibrary.Models
{
    public class AdminAuthor
    {
        public int? Id { get; set; }
        [Required, MinLength(4), MaxLength(30)]
        public string Name { get; set; }
        [Required]
        public int CountryId { get; set; }
        public bool IsEditPage { get; set; }
    }
}