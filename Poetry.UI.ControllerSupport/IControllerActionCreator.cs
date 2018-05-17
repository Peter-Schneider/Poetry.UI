using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.ControllerSupport
{
    public interface IControllerActionCreator
    {
        IEnumerable<ControllerAction> Create(Type controllerType);
    }
}
