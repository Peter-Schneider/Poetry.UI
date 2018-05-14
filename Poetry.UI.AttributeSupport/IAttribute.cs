using System;
using System.Collections.Generic;

namespace Poetry.UI.AttributeSupport
{
    public interface IAttribute
    {
        string Name { get; }
        Dictionary<string, string> Data { get; }
    }
}
