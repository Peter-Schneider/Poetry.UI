using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.ComponentSupport
{
    public class TypeIsMissingComponentAttributeException : Exception
    {
        public TypeIsMissingComponentAttributeException(Type type) :
            base($"Type {type} is missing the [Component(id)] attribute ({typeof(ComponentAttribute)}) to be added as a component")
        {

        }
    }
}
