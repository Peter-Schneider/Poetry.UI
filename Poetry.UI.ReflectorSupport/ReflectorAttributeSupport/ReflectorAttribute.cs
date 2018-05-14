using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Poetry.UI.ReflectorSupport.ReflectorAttributeSupport
{
    public class ReflectorAttribute : IReflectorAttribute, IReflectorAttributeData
    {
        public string Name { get; }
        public IDictionary<string, string> Attributes { get; }

        public ReflectorAttribute(string name, Dictionary<string, string> attributes)
        {
            Name = name;
            Attributes = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>(attributes));
        }
    }
}
