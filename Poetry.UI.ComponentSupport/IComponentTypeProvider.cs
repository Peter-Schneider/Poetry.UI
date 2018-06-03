using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.ComponentSupport
{
    public interface IComponentTypeProvider
    {
        IEnumerable<Type> GetTypes();
    }
}
