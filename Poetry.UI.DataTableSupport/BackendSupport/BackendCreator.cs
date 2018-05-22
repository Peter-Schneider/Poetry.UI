using Poetry.UI.DependencyInjectionSupport;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Poetry.UI.DataTableSupport.BackendSupport
{
    public class BackendCreator
    {
        IInstantiator Instantiator { get; }

        public BackendCreator(IInstantiator instantiator)
        {
            Instantiator = instantiator;
        }

        public IDictionary<string, IBackend> Create(IEnumerable<Type> types)
        {
            var result = new Dictionary<string, IBackend>();

            foreach(var type in types)
            {
                var attribute = type.GetCustomAttribute<DataTableBackendAttribute>();

                result.Add(attribute.Id, (IBackend)Instantiator.Instantiate(type));
            }

            return result;
        }
    }
}
