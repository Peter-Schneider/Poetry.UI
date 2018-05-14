using System.Collections.Generic;

namespace Poetry.UI.ReflectorSupport.ReflectorAttributeSupport
{
    public interface IReflectorAttributeData
    {
        IDictionary<string, string> Attributes { get; }
    }
}