using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.ReflectorSupport.ReflectorAttributeSupport
{
    public class ReflectorAttribute
    {
        public string Id { get; }

        public ReflectorAttribute(string id)
        {
            Id = id;
        }
    }
}
