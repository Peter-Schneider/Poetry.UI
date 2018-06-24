using Poetry.UI.EmbeddedResourceSupport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.ComponentSupport.EmbeddedResourceSupport
{
    public interface IPublicComponentEmbeddedResourceProvider
    {
        EmbeddedResource GetFile(string path);
    }
}
