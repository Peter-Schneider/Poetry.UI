using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.DependencyInjectionSupport
{
    public interface IDependencyInjectorProvider
    {
        IEnumerable<IDependencyInjector> GetAll();
    }
}
