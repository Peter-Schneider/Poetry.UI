using System.Collections.Generic;

namespace Poetry.UI.DataTableSupport.BackendSupport
{
    public interface IBackendCreator
    {
        IDictionary<string, IBackend> Create();
    }
}