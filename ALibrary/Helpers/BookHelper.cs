using ALibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace ALibrary.Helpers
{
    public static class BookHelper
    {
        public static string GetParentCategory(List<Category> categories)
        {
            var parentCategory = "empty";
            var category = categories.FirstOrDefault();
            using (var context = new DataContext())
            {
                if(category.ParentId != null)
                {
                    parentCategory = context.Categories.FirstOrDefault(c => c.Id == category.ParentId).Slug;
                }
            }

            return parentCategory;
        }

        public static string GetSubcategory(List<Category> categories)
        {
            return categories.FirstOrDefault().Slug;
        }
    }
}