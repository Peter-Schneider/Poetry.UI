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
            container.RegisterType<IBackendTypeProvider, BackendTypeProvider>();
            container.RegisterType<IBackendCreator, BackendCreator>();
            container.RegisterType<IBackendProvider, BackendProvider>();
        }
    }
}
