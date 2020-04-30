using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ALibrary.Helpers
{
    public class ListFileForrmatAttribute : ValidationAttribute
    {
        private string[] formats;
        public ListFileForrmatAttribute(string format)
        {
            formats = format.Split('|');
        }

        public override bool IsValid(object value)
        {
            var files = value as IEnumerable<HttpPostedFileBase>;

            foreach(var file in files)
            {
                if (file != null)
                {
                    string format = System.IO.Path.GetExtension(file.FileName.ToLower());
                    if (!formats.Contains(format))
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            this.ErrorMessage = "Неверный формат! Допустимые форматы: " + string.Join(", ", formats);
            return false;
        }
    }
}