using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.EmbeddedResourceSupport
{
    public interface IEmbeddedResourceAssemblyProvider
    {
        IEnumerable<EmbeddedResourceAssembly> GetAll();
    }
}
