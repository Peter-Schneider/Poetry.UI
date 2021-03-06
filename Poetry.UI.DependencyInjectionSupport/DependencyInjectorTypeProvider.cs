﻿using Poetry.UI.ReflectionSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Poetry.UI.DependencyInjectionSupport
{
    public class DependencyInjectorTypeProvider : IDependencyInjectorTypeProvider
    {
        IEnumerable<AssemblyWrapper> Assemblies { get; }

        public DependencyInjectorTypeProvider(IAssemblyProvider assemblyProvider)
        {
            Assemblies = assemblyProvider.GetAll().ToList().AsReadOnly();
        }

        public IEnumerable<Type> GetAll()
        {
            return Assemblies.SelectMany(a => a.Types.Where(t => t.GetCustomAttribute<DependencyInjectorAttribute>() != null));
        }
    }
}
