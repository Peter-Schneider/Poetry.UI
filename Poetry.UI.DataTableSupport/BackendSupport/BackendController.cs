using Poetry.UI.ControllerSupport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.DataTableSupport.BackendSupport
{
    [Controller("Backend")]
    public class BackendController
    {
        IBackendProvider BackendProvider { get; }

        public BackendController(IBackendProvider backendProvider)
        {
            BackendProvider = backendProvider;
        }

        [Action("GetAll")]
        public Result GetAll(string provider, int page, string sortBy, string sortDirection)
        {
            var direction = sortDirection == "ascending" ? (SortDirection?)SortDirection.Ascending : sortDirection == "descending" ? (SortDirection?)SortDirection.Descending : null;

            return BackendProvider.GetFor(provider).GetAll(new Query(page, sortBy, direction));
        }
    }
}
