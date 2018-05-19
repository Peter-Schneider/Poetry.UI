using Newtonsoft.Json;
using Poetry.UI.ControllerSupport;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace Poetry.UI.RoutingSupport
{
    public class MethodParameterValueProvider : IMethodParameterValueProvider
    {
        IDictionary<string, string> QueryString { get; }
        Func<string> RequestBody { get; }

        public MethodParameterValueProvider(IDictionary<string, string> queryString, Func<string> requestBody)
        {
            QueryString = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>(queryString));
            RequestBody = requestBody;
        }

        public object GetValue(ParameterInfo parameter)
        {
            if(parameter.GetCustomAttribute<FromRequestBodyAttribute>() != null)
            {
                return JsonConvert.DeserializeObject(RequestBody(), parameter.ParameterType);
            }

            if (!QueryString.ContainsKey(parameter.Name))
            {
                return null;
            }

            if (parameter.ParameterType == typeof(string))
            {
                return QueryString[parameter.Name];
            }

            if (parameter.ParameterType == typeof(int))
            {
                return int.Parse(QueryString[parameter.Name], CultureInfo.InvariantCulture);
            }

            if (parameter.ParameterType == typeof(double))
            {
                return double.Parse(QueryString[parameter.Name], CultureInfo.InvariantCulture);
            }

            return null;
        }
    }
}
