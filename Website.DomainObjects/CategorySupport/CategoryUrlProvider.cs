using System;
using System.Collections.Generic;
using System.Text;

namespace Website.CategorySupport
{
    public class CategoryUrlProvider : ICategoryUrlProvider
    {
        public string GetUrl(Category category)
        {
            return $"/{category.UrlSegment}";
        }
    }
}
