using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Poetry.UI.ControllerSupport
{
    public class ControllerCreator : IControllerCreator
    {
        IControllerActionCreator ControllerActionCreator { get; }

        public ControllerCreator(IControllerActionCreator controllerActionCreator)
        {
            ControllerActionCreator = controllerActionCreator;
        }

        public Controller Create(Type type)
        {
            var attribute = type.GetCustomAttribute<ControllerAttribute>();

            return new Controller(attribute.Id, type, ControllerActionCreator.Create(type).ToArray());
        }
    }
}
