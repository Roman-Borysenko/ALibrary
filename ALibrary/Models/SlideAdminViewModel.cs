using ALibrary.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace ALibrary.Models
{
    public class SlideAdminViewModel
    {
        public int Id { get; set; }
        [Required, StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }
        [Required, StringLength(120, MinimumLength = 12)]
        public string Signature { get; set; }
        [Required, MinLength(3), AllowHtml]
        public string Description { get; set; }
        [Required, StringLength(120, MinimumLength = 12)]
        public string LinkText { get; set; }
        [Required, StringLength(120, MinimumLength = 12)]
        public string Link { get; set; }
        public string ImagePath { get; set; }
        [FileFormat(".jpg|.jpeg|.png")]
        public HttpPostedFileBase Image { get; set; }
    }
}