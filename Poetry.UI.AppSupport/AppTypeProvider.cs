using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Poetry.UI.AppSupport
{
    public class AppTypeProvider : IAppTypeProvider
    {
        IEnumerable<Assembly> Assemblies { get; }

        public AppTypeProvider(IEnumerable<Assembly> assemblies)
        {
            Assemblies = assemblies.ToList().AsReadOnly();
        }

        public IEnumerable<Type> GetTypes()
        {
            return Assemblies.SelectMany(a => a.ExportedTypes).Where(t => CustomAttributeExtensions.GetCustomAttribute<AppAttribute>(t) != null);
        }
    }
}
