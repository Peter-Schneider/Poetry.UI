using Poetry.UI.ControllerSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Poetry.UI.ComponentSupport
{
    public class ComponentControllerTypeProvider : IComponentControllerTypeProvider
    {
        public IEnumerable<Type> GetTypes(Type componentType)
        {
            return componentType.Assembly.GetTypes().Where(t => t.Name.EndsWith("Controller")).Where(t => t.GetCustomAttribute<ControllerAttribute>() != null);
        }
    }
}
