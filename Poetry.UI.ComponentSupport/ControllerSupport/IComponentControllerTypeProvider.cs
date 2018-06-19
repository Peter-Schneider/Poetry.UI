using System;
using System.Collections.Generic;

namespace Poetry.UI.ControllerSupport
{
    public interface IComponentControllerTypeProvider
    {
        IEnumerable<Type> GetTypes(Type componentType);
    }
}