using Poetry.UI.DependencyInjectionSupport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Website.CategorySupport
{
    [DependencyInjector]
    public class CategorySupportDependencyInjector : IDependencyInjector
    {
        public void InjectDependencies(IContainer container)
        {
            container.RegisterSingleton<ICategoryRepository, CategoryRepository>();
            container.RegisterSingleton<ICategoryUrlProvider, CategoryUrlProvider>();
        }
    }
}
