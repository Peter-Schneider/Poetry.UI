using Poetry.UI.ControllerSupport;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Poetry.UI.ComponentSupport
{
    public class ComponentCreator
    {
        IControllerCreator ControllerCreator { get; }

        public ComponentCreator(IControllerCreator controllerCreator)
        {
            ControllerCreator = controllerCreator;
        }

        public Component Create(Type type)
        {
            var attribute = type.GetTypeInfo().GetCustomAttribute<ComponentAttribute>();

            return new Component(attribute.Id);
        }
    }
}
