using Poetry.UI.ReflectionSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Poetry.UI.DataTableSupport.BackendSupport
{
    public class BackendTypeProvider : IBackendTypeProvider
    {
        IEnumerable<AssemblyWrapper> Assemblies { get; }

        public BackendTypeProvider(IAssemblyProvider assemblyProvider)
        {
            Assemblies = assemblyProvider.GetAll().ToList().AsReadOnly();
        }

        public IEnumerable<Type> GetTypes()
        {
            return Assemblies.SelectMany(a => a.Types.Where(t => t.Name.EndsWith("DataTableBackend")).Where(t => t.GetCustomAttribute<DataTableBackendAttribute>() != null));
        }
    }
}
