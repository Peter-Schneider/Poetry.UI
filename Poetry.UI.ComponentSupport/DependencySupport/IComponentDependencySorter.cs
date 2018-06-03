using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.ComponentSupport.DependencySupport
{
    public interface IComponentDependencySorter
    {
        IEnumerable<Component> Sort(IEnumerable<Component> components);
    }
}
