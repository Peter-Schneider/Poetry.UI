using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Poetry.UI.DataTableSupport.BackendSupport
{
    public class BackendTypeProvider : IBackendTypeProvider
    {
        IEnumerable<Assembly> Assemblies { get; }

        public BackendTypeProvider(IEnumerable<Assembly> assemblies)
        {
            Assemblies = assemblies;
        }

        public IEnumerable<Type> GetTypes()
        {
            return Assemblies.SelectMany(a => a.GetTypes().Where(t => t.Name.EndsWith("DataTableBackend")).Where(t => t.GetCustomAttribute<DataTableBackendAttribute>() != null));
        }
    }
}
