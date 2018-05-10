using System;
using System.Collections.Generic;
using System.IO;

namespace Poetry.UI.EmbeddedResourceSupport
{
    public class EmbeddedResource
    {
        public string Name { get; }

        public EmbeddedResource(string name)
        {
            Name = name;
        }

        public override bool Equals(object obj)
        {
            var resource = obj as EmbeddedResource;
            return resource != null &&
                   Name == resource.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}