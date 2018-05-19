using Poetry.UI.AttributeSupport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.ComponentSupport
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ComponentAttribute : Attribute, IAttribute {
        public string Id { get; }

        public ComponentAttribute(string id)
        {
            Id = id;
        }

        public string Name => "Component";

        public Dictionary<string, string> Data => new Dictionary<string, string>
        {
            [nameof(Id)] = Id
        };
    }
}
