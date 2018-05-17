using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poetry.UI.ControllerSupport;

namespace Poetry.UI.ComponentSupport
{
    public class ComponentControllerCreator : IComponentControllerCreator
    {
        IComponentControllerTypeProvider ComponentControllerTypeProvider { get; }
        IControllerCreator ControllerCreator { get; }

        public ComponentControllerCreator(IComponentControllerTypeProvider componentControllerTypeProvider, IControllerCreator controllerCreator)
        {
            ComponentControllerTypeProvider = componentControllerTypeProvider;
            ControllerCreator = controllerCreator;
        }

        public IEnumerable<Controller> Create(Type componentType)
        {
            return ComponentControllerTypeProvider.GetTypes(componentType).Select(ControllerCreator.Create).ToList();
        }
    }
}
