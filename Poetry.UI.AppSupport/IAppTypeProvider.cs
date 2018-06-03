using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.AppSupport
{
    public interface IAppTypeProvider
    {
        IEnumerable<Type> GetTypes();
    }
}
