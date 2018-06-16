using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.DependencyInjectionSupport
{
    public class DependencyInjectorCreator : IDependencyInjectorCreator
    {
        IInstantiator Instantiator { get; }
        IDependencyInjectorTypeProvider DependencyInjectorTypeProvider { get; }

        public DependencyInjectorCreator(IInstantiator instantiator, IDependencyInjectorTypeProvider dependencyInjectorTypeProvider)
        {
            Instantiator = instantiator;
            DependencyInjectorTypeProvider = dependencyInjectorTypeProvider;
        }

        public IEnumerable<IDependencyInjector> CreateAll()
        {
            var result = new List<IDependencyInjector>();

            foreach (var type in DependencyInjectorTypeProvider.GetAll())
            {
                result.Add((IDependencyInjector)Instantiator.Instantiate(type));
            }

            return result;
        }
    }
}
