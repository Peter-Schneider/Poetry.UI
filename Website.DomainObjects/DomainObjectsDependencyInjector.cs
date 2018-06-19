using Poetry.UI.DependencyInjectionSupport;
using System;
using System.Collections.Generic;
using System.Text;
using Website.CategorySupport;
using Website.ProductSupport;

namespace Website.DomainObjects
{
    [DependencyInjector]
    public class DomainObjectsDependencyInjector : IDependencyInjector
    {
        public void InjectDependencies(IContainer container)
        {
            container.RegisterSingleton<ICategoryUrlProvider, CategoryUrlProvider>();
            container.RegisterSingleton<ICategoryRepository, CategoryRepository>();
            container.RegisterSingleton<IProductUrlProvider, ProductUrlProvider>();
            container.RegisterSingleton<IProductRepository, ProductRepository>();
        }
    }
}
