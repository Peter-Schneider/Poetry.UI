using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.DependencyInjectionSupport
{
    public interface IDependencyInjector
    {
        void InjectDependencies(IContainer container);
    }
}
