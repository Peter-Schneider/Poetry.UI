using System;
using System.Collections.Generic;
using System.IO;

namespace Poetry.UI.EmbeddedResourceSupport
{
    /// <summary>
    /// Represents an assembly embedded resource stream by its identifier.
    /// </summary>
    public class EmbeddedResource
    {
        /// <summary>
        /// The embedded resource name. Note that this includes the assembly name.
        /// </summary>
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