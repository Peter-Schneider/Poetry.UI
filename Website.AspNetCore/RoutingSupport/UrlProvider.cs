using Website.CategorySupport;
using Website.ProductSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.RoutingSupport
{
    public class UrlProvider
    {
        CategoryRepository CategoryRepository { get; }
        ProductRepository ProductRepository { get; }

        public UrlProvider(CategoryRepository categoryRepository, ProductRepository productRepository)
        {
            CategoryRepository = categoryRepository;
            ProductRepository = productRepository;
        }

        public string GetUrl(Category category)
        {
            return $"/{category.UrlSegment}";
        }

        public string GetUrl(Product product)
        {
            var category = CategoryRepository.Get(product.CategoryId);

            return $"/{category.UrlSegment}/{product.ArticleNo}";
        }
    }
}