using System;
using System.Collections.Generic;

namespace Poetry.UI.DataTableSupport.BackendSupport
{
    public interface IBackendTypeProvider
    {
        IEnumerable<Type> GetTypes();
    }
}