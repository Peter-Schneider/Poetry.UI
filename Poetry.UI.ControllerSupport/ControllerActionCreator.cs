using Poetry.UI.MvcSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Poetry.UI.ControllerSupport
{
    public class ControllerActionCreator : IControllerActionCreator
    {
        public IEnumerable<ControllerAction> Create(Type controllerType)
        {
            foreach(var method in controllerType.GetMethods())
            {
                var attribute = method.GetCustomAttribute<ActionAttribute>();

                if(attribute == null)
                {
                    continue;
                }

                yield return new ControllerAction(attribute.Id);
            }
        }
    }
}
