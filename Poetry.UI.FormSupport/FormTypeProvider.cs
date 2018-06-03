using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Poetry.UI.FormSupport
{
    public class FormTypeProvider : IFormTypeProvider
    {
        IEnumerable<Assembly> Assemblies { get; }

        public FormTypeProvider(IEnumerable<Assembly> assemblies)
        {
            Assemblies = assemblies.ToList().AsReadOnly();
        }

        public IEnumerable<Type> GetTypes()
        {
            return Assemblies.SelectMany(a => a.GetTypes()).Where(t => t.GetCustomAttribute<FormAttribute>() != null);
        }
    }
}
