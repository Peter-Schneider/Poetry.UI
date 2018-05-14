using Poetry.UI.AttributeSupport;
using System;
using System.Collections.Generic;

namespace Poetry.UI.MvcSupport
{
    public class ControllerAttribute : Attribute, IAttribute
    {
        public string Id { get; }

        public ControllerAttribute(string id)
        {
            Id = id;
        }

        public string Name => "Controller";

        public Dictionary<string, string> Data => new Dictionary<string, string>
        {
            [nameof(Id)] = Id
        };
    }
}
