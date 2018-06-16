using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poetry.UI.DependencyInjectionSupport
{
    public class DependencyInjectorProvider : IDependencyInjectorProvider
    {
        IEnumerable<IDependencyInjector> DependencyInjectors { get; }

        public DependencyInjectorProvider(IDependencyInjectorCreator dependencyInjectorCreator)
        {
            DependencyInjectors = dependencyInjectorCreator.CreateAll().ToList().AsReadOnly();
        }

        public IEnumerable<IDependencyInjector> GetAll()
        {
            return DependencyInjectors;
        }
    }
}
