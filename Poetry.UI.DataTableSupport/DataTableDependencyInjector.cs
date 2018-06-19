using Poetry.UI.DataTableSupport.BackendSupport;
using Poetry.UI.DependencyInjectionSupport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.DataTableSupport
{
    [DependencyInjector]
    public class DataTableDependencyInjector : IDependencyInjector
    {
        public void InjectDependencies(IContainer container)
        {
            container.RegisterSingleton<IBackendTypeProvider, BackendTypeProvider>();
            container.RegisterSingleton<IBackendCreator, BackendCreator>();
            container.RegisterSingleton<IBackendProvider, BackendProvider>();
        }
    }
}
