using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Poetry.UI.PageEditingSupport
{
    public class ObjectIdentifier : IObjectIdentifier
    {
        public string GetId(object @object)
        {
            var getter = @object.GetType().GetProperty("Id")?.GetGetMethod();

            if(getter == null)
            {
                throw new ObjectCouldNotBeIdentifiedException($"Could not identify object {@object}. Getting a public property called Id was attempted. Either implement a public Id property of type string, int or Guid, or inject your own IObjectIdentifier by using AddPoetryUI().Inject<IObjectIdentifier, YourType>()");
            }

            var result = getter.Invoke(@object, new object[] { });

            if(result is IConvertible)
            {
                return ((IConvertible)result).ToString(CultureInfo.InvariantCulture);
            }

            throw new ObjectCouldNotBeConvertedToStringException($"Could not convert Id result to string (on {@object}). Either implement a public Id property of type string, int or Guid, or inject your own IObjectIdentifier by using AddPoetryUI().Inject<IObjectIdentifier, YourType>()");
        }
    }
}
