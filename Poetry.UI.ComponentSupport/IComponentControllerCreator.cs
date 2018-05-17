using Poetry.UI.ControllerSupport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.ComponentSupport
{
    public interface IComponentControllerCreator
    {
        IEnumerable<Controller> Create(Type componentType);
    }
}
