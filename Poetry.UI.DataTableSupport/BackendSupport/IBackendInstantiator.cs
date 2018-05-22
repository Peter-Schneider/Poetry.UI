using System;

namespace Poetry.UI.DataTableSupport.BackendSupport
{
    public interface IBackendInstantiator
    {
        IBackend Instantiate(Type type);
    }
}