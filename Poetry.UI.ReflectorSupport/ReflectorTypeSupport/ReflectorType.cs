using Poetry.UI.ReflectorSupport.ReflectorAttributeSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poetry.UI.ReflectorSupport.ReflectorTypeSupport
{
    public class ReflectorType
    {
        public string Id { get; }
        public IEnumerable<Attribute> Attributes { get; }

        public ReflectorType(string id, IEnumerable<Attribute> attributes)
        {
            Id = id;
            Attributes = attributes.ToList().AsReadOnly();
        }
    }
}
