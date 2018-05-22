using Poetry.UI.DataTableSupport;
using Poetry.UI.DataTableSupport.BackendSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.Apps.KeyFigureSupport
{
    [DataTableBackend("key-figure")]
    public class KeyFigureDataTableBackend : IBackend
    {
        int PageSize { get; } = 4;

        public Result GetAll(Query query)
        {
            return new Result(
                PageSize,
                KeyFiguresController.Items.Skip(PageSize * (query.Page - 1)).Take(PageSize),
                KeyFiguresController.Items.Count
            );
        }
    }
}