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

        public AppTypeProvider(IEnumerable<AssemblyWrapper> assemblies)
        {
            Assemblies = assemblies.ToList().AsReadOnly();
        }

        public IEnumerable<Type> GetTypes()
        {
            return Assemblies.SelectMany(a => a.Types).Where(t => CustomAttributeExtensions.GetCustomAttribute<AppAttribute>(t) != null);
        }
    }
}
