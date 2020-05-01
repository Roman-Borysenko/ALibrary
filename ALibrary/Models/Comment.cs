using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ALibrary.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string UserId { get; set; }
        public Book Book { get; set; }
        public Article Article { get; set; }
        [NotMapped]
        public ApplicationUser GetUser 
        {
            get 
            {
                using (var context = new ApplicationDbContext())
                {
                    return context.Users.Find(UserId);
                }
            }
        }
        public DateTime Create { get; set; }
    }
}