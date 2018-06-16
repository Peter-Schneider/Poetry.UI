using Poetry.UI.ReflectionSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Poetry.UI.ComponentSupport
{
    public class ComponentTypeProvider : IComponentTypeProvider
    {
        IEnumerable<AssemblyWrapper> Assemblies { get; }

        public ComponentTypeProvider(IAssemblyProvider assemblyProvider)
        {
            Assemblies = assemblyProvider.GetAll().ToList().AsReadOnly();
        }

        public IEnumerable<Type> GetTypes()
        {
            return Assemblies.SelectMany(a => a.Types).Where(t => t.GetCustomAttribute<ComponentAttribute>() != null);
        }
    }
}
