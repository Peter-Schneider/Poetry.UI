using System;
using System.Collections.Generic;
using System.Text;
using Website.CategorySupport;

namespace Website.ProductSupport
{
    public class ProductUrlProvider : IProductUrlProvider
    {
        ICategoryRepository CategoryRepository { get; }

        public ProductUrlProvider(ICategoryRepository categoryRepository)
        {
            CategoryRepository = categoryRepository;
        }

        public string GetUrl(Product product)
        {
            var category = CategoryRepository.Get(product.CategoryId);

            if(category == null)
            {
                return null;
            }

            return $"/{category.UrlSegment}/{product.ArticleNo}";
        }
    }
}
