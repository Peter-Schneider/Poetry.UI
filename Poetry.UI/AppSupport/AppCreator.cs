using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Poetry.UI.AppSupport //
{
    public class AppCreator
    {
        public IEnumerable<App> Create(IEnumerable<Assembly> assemblies)
        {
            var types = assemblies
                    .SelectMany(a => a.ExportedTypes);

            foreach (var type in types)
            {
                var attribute = type.GetTypeInfo().GetCustomAttribute<AppAttribute>();

                if (attribute == null)
                {
                    continue;
                }

                yield return new App(attribute.Id, type.GetTypeInfo().GetCustomAttributes<ScriptAttribute>().Select(s => new Script(s.Src, s.Order)), type.GetTypeInfo().GetCustomAttributes<StyleAttribute>().Select(s => s.Href));
            }
        }
    }
}
