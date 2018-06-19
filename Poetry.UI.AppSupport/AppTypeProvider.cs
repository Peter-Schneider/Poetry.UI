using Poetry.UI.ComponentSupport;
using Poetry.UI.ReflectionSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Poetry.UI.AppSupport
{
    public class AppTypeProvider : IAppTypeProvider
    {
        IEnumerable<AssemblyWrapper> Assemblies { get; }

        public AppTypeProvider(IAssemblyProvider assemblyProvider)
        {
            Assemblies = assemblyProvider.GetAll().ToList().AsReadOnly();
        }

        public IEnumerable<Type> GetTypes(Component component)
        {
            return component.Assembly.Types.Where(t => CustomAttributeExtensions.GetCustomAttribute<AppAttribute>(t) != null);
        }
    }
}
