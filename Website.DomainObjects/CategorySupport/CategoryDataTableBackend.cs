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

        ICategoryRepository CategoryRepository { get; }
        ICategoryUrlProvider CategoryUrlProvider { get; }

        public CategoryDataTableBackend(ICategoryRepository categoryRepository, ICategoryUrlProvider categoryUrlProvider)
        {
            CategoryRepository = categoryRepository;
            CategoryUrlProvider = categoryUrlProvider;
        }

        public Result GetAll(Query query)
        {
            var items = CategoryRepository.GetAll();
            var sortBy = (Func<Category, object>)(c => TypeAccessor[c, query.SortBy ?? "Name"]);

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
                items.Skip(PageSize * (query.Page - 1)).Take(PageSize).Select(item => new { Item = item, Url = CategoryUrlProvider.GetUrl(item) }),
                items.Count()
            );
        }
    }
}