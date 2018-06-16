using Poetry.UI.DependencyInjectionSupport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.AppSupport
{
    public class AppSupportDependencyInjector : IDependencyInjector
    {
        public void InjectDependencies(IContainer container)
        {
            container.RegisterType<IAppCreator, AppCreator>();
            container.RegisterType<IAppRepository, AppRepository>();
        }
    }
}
