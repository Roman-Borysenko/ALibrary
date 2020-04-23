using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ALibrary.Helpers
{
    public class FileFormatAttribute : ValidationAttribute
    {
        private string[] formats;
        public FileFormatAttribute(string format)
        {
            formats = format.Split('|');
        }

        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;
            if(file != null)
            {
                string format = System.IO.Path.GetExtension(file.FileName.ToLower());
                if (formats.Contains(format))
                {
                    return true;
                }
            } else
            {
                return true;
            }
            this.ErrorMessage = "Неверный формат! Допустимые форматы: " + string.Join(", ", formats);
            return false;
        }
    }
}