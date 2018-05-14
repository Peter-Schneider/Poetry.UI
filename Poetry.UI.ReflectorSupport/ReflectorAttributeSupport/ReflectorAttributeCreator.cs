using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Poetry.UI.ReflectorSupport.ReflectorAttributeSupport
{
    public class ReflectorAttributeCreator : IReflectorAttributeCreator
    {
        public IEnumerable<Attribute> CreateReflectorAttributes(Type type)
        {
            return type.GetTypeInfo().GetCustomAttributes();
        }
    }
}
