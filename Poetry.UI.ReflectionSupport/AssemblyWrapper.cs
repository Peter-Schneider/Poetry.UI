using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Poetry.UI.ReflectionSupport
{
    public class AssemblyWrapper
    {
        public Assembly Assembly { get; }
        public IEnumerable<Type> Types { get; }

        public AssemblyWrapper(Assembly assembly)
        {
            Assembly = assembly;
            Types = assembly.ExportedTypes.ToList().AsReadOnly();
        }

        public AssemblyWrapper(IEnumerable<Type> types)
        {
            Types = types.ToList().AsReadOnly();
        }
    }
}
