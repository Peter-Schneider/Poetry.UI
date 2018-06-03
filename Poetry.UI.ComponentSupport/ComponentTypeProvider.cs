using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Poetry.UI.ComponentSupport
{
    public class ComponentTypeProvider : IComponentTypeProvider
    {
        IEnumerable<Assembly> Assemblies { get; }

        public ComponentTypeProvider(IEnumerable<Assembly> assemblies)
        {
            Assemblies = assemblies.ToList().AsReadOnly();
        }

        public IEnumerable<Type> GetTypes()
        {
            return Assemblies.SelectMany(a => a.GetTypes()).Where(t => t.GetCustomAttribute<ComponentAttribute>() != null);
        }
    }
}
