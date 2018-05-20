using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Poetry.UI.FormSupport
{
    public class FormTypeProvider
    {
        public IEnumerable<Type> GetTypes(params Assembly[] assemblies)
        {
            return assemblies.SelectMany(a => a.GetTypes()).Where(t => t.GetCustomAttribute<FormAttribute>() != null);
        }
    }
}
