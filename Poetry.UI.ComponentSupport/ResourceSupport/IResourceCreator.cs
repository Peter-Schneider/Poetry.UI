using Poetry.UI.ComponentSupport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.ResourceSupport
{
    public interface IResourceCreator
    {
        IEnumerable<Resource> Create(string componentId, Type ownerType);
    }
}
