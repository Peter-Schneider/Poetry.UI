using Poetry.UI.ReflectorSupport.ReflectorTypeSupport;
using System;

namespace Poetry.UI.ReflectorSupport
{
    public interface IReflector
    {
        ReflectorType GetReflectorType(Type type);
    }
}
