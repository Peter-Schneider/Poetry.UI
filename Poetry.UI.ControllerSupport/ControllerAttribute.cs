using System;
using System.Collections.Generic;

namespace Poetry.UI.ControllerSupport
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ControllerAttribute : Attribute
    {
        public string Id { get; }

        public ControllerAttribute(string id)
        {
            Id = id;
        }
    }
}
