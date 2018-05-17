using System;
using System.Collections.Generic;

namespace Poetry.UI.ComponentSupport
{
    public interface IComponentControllerTypeProvider
    {
        IEnumerable<Type> GetTypes(Type componentType);
    }
}