using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Poetry.UI.DataTableSupport.BackendSupport
{
    public class BackendTypeProvider
    {
        public IEnumerable<Type> GetTypes(IEnumerable<Assembly> assemblies)
        {
            return assemblies.SelectMany(a => a.GetTypes().Where(t => t.Name.EndsWith("DataTableBackend")).Where(t => t.GetCustomAttribute<DataTableBackendAttribute>() != null));
        }
    }
}
