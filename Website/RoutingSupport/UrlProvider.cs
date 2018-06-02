using Website.CategorySupport;
using Website.ProductSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.RoutingSupport
{
    public static class UrlProvider
    {
        public static string GetUrl(Category category)
        {
            return $"/{category.UrlSegment}";
        }

        public static string GetUrl(Product product)
        {
            var category = CategoryRepository.Get(product.CategoryId);

            return $"/{category.UrlSegment}/{product.ArticleNo}";
        }
    }
}