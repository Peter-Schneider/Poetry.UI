using Poetry.UI.DependencyInjectionSupport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Website.ProductSupport
{
    [DependencyInjector]
    public class ProductSupportDependencyInjector : IDependencyInjector
    {
        public void InjectDependencies(IContainer container)
        {
            container.RegisterSingleton<IProductRepository, ProductRepository>();
            container.RegisterSingleton<IProductUrlProvider, ProductUrlProvider>();
        }
    }
}
