using Poetry.UI.AttributeSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poetry.UI.AppSupport
{
    public class AppAttribute : Attribute, IAttribute {
        public string Id { get; }

        public AppAttribute(string id)
        {
            Id = id;
        }

        public string Name => "App";

        public Dictionary<string, string> Data => new Dictionary<string, string>
        {
            [nameof(Id)] = Id
        };
    }
}
