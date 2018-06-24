using Poetry.UI.ControllerSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Poetry.UI.ControllerSupport
{
    public class ComponentControllerTypeProvider : IComponentControllerTypeProvider
    {
        public IEnumerable<Type> GetTypes(Type componentType)
        {
            return componentType.Assembly.ExportedTypes.Where(t => t.Name.EndsWith("Controller")).Where(t => t.GetCustomAttribute<ControllerAttribute>() != null);
        }
    }
}
