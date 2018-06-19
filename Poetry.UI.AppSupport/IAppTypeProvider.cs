using Poetry.UI.ComponentSupport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.AppSupport
{
    public interface IAppTypeProvider
    {
        IEnumerable<Type> GetTypes(Component component);
    }
}
