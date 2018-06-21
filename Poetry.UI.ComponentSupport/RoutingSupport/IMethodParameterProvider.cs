using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Poetry.UI.RoutingSupport
{
    public interface IMethodParameterProvider
    {
        object[] GetParameters(MethodInfo method);
    }
}
