using Poetry.UI.DependencyInjectionSupport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.PageEditingSupport
{
    [DependencyInjector]
    public class PageEditingSupportDependencyInjector : IDependencyInjector
    {
        public void InjectDependencies(IContainer container)
        {
            container.RegisterSingleton<IObjectIdentifier, ObjectIdentifier>();
            container.RegisterSingleton<IPropertyExpressionMetaDataProvider, PropertyExpressionMetaDataProvider>();
            container.RegisterSingleton<IPathPrefixProvider, PathPrefixProvider>();
        }
    }
}
