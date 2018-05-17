using Poetry.UI.ControllerSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Poetry.UI.ComponentSupport
{
    public class ComponentCreator
    {
        IComponentControllerCreator ComponentControllerCreator { get; }

        public ComponentCreator(IComponentControllerCreator componentControllerCreator)
        {
            ComponentControllerCreator = componentControllerCreator;
        }

        public Component Create(Type type)
        {
            var attribute = type.GetTypeInfo().GetCustomAttribute<ComponentAttribute>();

            return new Component(attribute.Id, type.Assembly, ComponentControllerCreator.Create(type).ToArray());
        }
    }
}
