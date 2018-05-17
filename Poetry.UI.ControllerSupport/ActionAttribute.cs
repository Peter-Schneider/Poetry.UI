using Poetry.UI.AttributeSupport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.ControllerSupport
{
    public class ActionAttribute : Attribute, IAttribute
    {
        public string Id { get; }

        public ActionAttribute(string id)
        {
            Id = id;
        }

        public string Name => "Action";

        public Dictionary<string, string> Data => new Dictionary<string, string>
        {
            [nameof(Id)] = Id
        };
    }
}
