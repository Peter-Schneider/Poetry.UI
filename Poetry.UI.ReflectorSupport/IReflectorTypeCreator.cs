using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.ReflectorSupport
{
    public interface IReflectorTypeCreator
    {
        ReflectorType CreateReflectorType(Type type);
    }
}
