using Website.CategorySupport;
using Website.RoutingSupport;
using Poetry.UI.DataTableSupport;
using Poetry.UI.DataTableSupport.BackendSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FastMember;

namespace Website.ProductSupport
{
    [DataTableBackend("product")]
    public class ProductDataTableBackend : IBackend
    {
        int PageSize { get; } = 4;
        TypeAccessor TypeAccessor { get; } = TypeAccessor.Create(typeof(Product));

        ProductRepository ProductRepository { get; }
        UrlProvider UrlProvider { get; }

        public ProductDataTableBackend(ProductRepository productRepository, UrlProvider urlProvider)
        {
            ProductRepository = productRepository;
            UrlProvider = urlProvider;
        }

        public Result GetAll(Query query)
        {
            var items = ProductRepository.GetAll();
            var sortBy = (Func<Product, object>)(c => TypeAccessor[c, query.SortBy ?? "Name"]);

            if (query.SortDirection == SortDirection.Descending)
            {
                items = items.OrderByDescending(sortBy);
            }
            else
            {
                items = items.OrderBy(sortBy);
            }

            return new Result(
                PageSize,
                items.Skip(PageSize * (query.Page - 1)).Take(PageSize).Select(item => new { Item = item, Url = UrlProvider.GetUrl(item) }),
                items.Count()
            );
        }
    }
}