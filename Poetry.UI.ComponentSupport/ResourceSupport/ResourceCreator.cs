using Poetry.UI.ComponentSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Poetry.UI.ResourceSupport
{
    public class ResourceCreator : IResourceCreator
    {
        public IEnumerable<Resource> Create(string componentId, Type ownerType)
        {
            return ownerType.GetCustomAttributes<ResourceAttribute>().Select(a => new Resource(componentId, a.Path)).ToList();
        }
    }
}
