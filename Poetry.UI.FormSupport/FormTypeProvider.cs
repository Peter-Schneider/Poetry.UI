using Poetry.UI.ReflectionSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Poetry.UI.FormSupport
{
    public class FormTypeProvider : IFormTypeProvider
    {
        IEnumerable<AssemblyWrapper> Assemblies { get; }

        public FormTypeProvider(IAssemblyProvider assemblyProvider)
        {
            Assemblies = assemblyProvider.GetAll().ToList().AsReadOnly();
        }

        public IEnumerable<Type> GetAll()
        {
            return Assemblies.SelectMany(a => a.Types).Where(t => t.GetCustomAttribute<FormAttribute>() != null);
        }
    }
}
