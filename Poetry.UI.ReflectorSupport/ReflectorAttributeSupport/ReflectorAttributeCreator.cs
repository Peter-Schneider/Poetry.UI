using Poetry.UI.AttributeSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Poetry.UI.ReflectorSupport.ReflectorAttributeSupport
{
    public class ReflectorAttributeCreator : IReflectorAttributeCreator
    {
        public IEnumerable<IReflectorAttribute> CreateReflectorAttributes(Type type)
        {
            foreach (var attribute in type.GetTypeInfo().GetCustomAttributes())
            {
                var iattribute = attribute as IAttribute;

                if (iattribute != null)
                {
                    yield return new ReflectorAttribute(iattribute.Name, iattribute.Data);
                }
                else
                {
                    var name = attribute.GetType().Name;

                    if (name.EndsWith("Attribute"))
                    {
                        name = name.Substring(0, name.Length - "Attribute".Length);
                    }

                    yield return new ReflectorAttributeContainer(name, attribute);
                }
            }
        }
    }
}
