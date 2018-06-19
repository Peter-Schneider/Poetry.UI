using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.ControllerSupport
{
    public interface IComponentControllerCreator
    {
        IEnumerable<Controller> Create(Type componentType);
    }
}
