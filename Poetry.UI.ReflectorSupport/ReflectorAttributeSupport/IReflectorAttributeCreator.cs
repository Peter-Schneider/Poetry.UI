using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.ReflectorSupport.ReflectorAttributeSupport
{
    public interface IReflectorAttributeCreator
    {
        IEnumerable<ReflectorAttribute> CreateReflectorAttributes(Type type);
    }
}
