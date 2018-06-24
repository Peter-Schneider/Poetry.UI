using Poetry.UI.AppSupport.EmbeddedResourceSupport;
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
            container.RegisterSingleton<IAppTypeProvider, AppTypeProvider>();
            container.RegisterSingleton<IAppCreator, AppCreator>();
            container.RegisterSingleton<IAppRepository, AppRepository>();
            container.RegisterSingleton<IPublicAppEmbeddedResourceProvider, PublicAppEmbeddedResourceProvider>();
        }
    }
}
