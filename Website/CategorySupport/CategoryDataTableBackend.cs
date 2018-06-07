using Website.RoutingSupport;
using Poetry.UI.DataTableSupport;
using Poetry.UI.DataTableSupport.BackendSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FastMember;

namespace Website.CategorySupport
{
    [DataTableBackend("category")]
    public class CategoryDataTableBackend : IBackend
    {
        int PageSize { get; } = 4;
        TypeAccessor TypeAccessor { get; } = TypeAccessor.Create(typeof(Category));

        public Result GetAll(Query query)
        {
            return new Result(
                PageSize,
                CategoryRepository.GetAll().OrderBy(c => TypeAccessor[c, query.SortBy ?? "Name"]).Skip(PageSize * (query.Page - 1)).Take(PageSize).Select(item => new { Item = item, Url = UrlProvider.GetUrl(item) }),
                CategoryRepository.GetAll().Count()
            );
        }
    }
}