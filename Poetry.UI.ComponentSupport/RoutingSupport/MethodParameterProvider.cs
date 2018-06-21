using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Poetry.UI.RoutingSupport
{
    public class MethodParameterProvider : IMethodParameterProvider
    {
        IMethodParameterValueProvider ControllerActionParameterValueProvider { get; }

        public MethodParameterProvider(IMethodParameterValueProvider controllerActionParameterValueProvider)
        {
            ControllerActionParameterValueProvider = controllerActionParameterValueProvider;
        }

        public object[] GetParameters(MethodInfo method)
        {
            var result = new List<object>();

            foreach(var parameter in method.GetParameters())
            {
                result.Add(ControllerActionParameterValueProvider.GetValue(parameter));
            }

            return result.ToArray();
        }
    }
}
