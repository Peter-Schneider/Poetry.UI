using System;
using System.Collections.Generic;

namespace Poetry.UI.ComponentSupport.DependencySupport
{
    public interface IComponentDependencyCreator
    {
        IEnumerable<string> Create(Type type);
    }
}