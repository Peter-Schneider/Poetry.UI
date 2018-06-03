using Poetry.UI.DependencyInjectionSupport;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Poetry.UI.DataTableSupport.BackendSupport
{
    public class BackendCreator : IBackendCreator
    {
        IInstantiator Instantiator { get; }
        IBackendTypeProvider BackendTypeProvider { get; }

        public BackendCreator(IInstantiator instantiator, IBackendTypeProvider backendTypeProvider)
        {
            Instantiator = instantiator;
            BackendTypeProvider = backendTypeProvider;
        }

        public IDictionary<string, IBackend> Create()
        {
            var result = new Dictionary<string, IBackend>();

            foreach(var type in BackendTypeProvider.GetTypes())
            {
                var attribute = type.GetCustomAttribute<DataTableBackendAttribute>();

                result.Add(attribute.Id, (IBackend)Instantiator.Instantiate(type));
            }

            return result;
        }
    }
}
