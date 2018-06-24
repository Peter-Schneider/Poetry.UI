using Poetry.UI.EmbeddedResourceSupport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.AppSupport.EmbeddedResourceSupport
{
    public interface IPublicAppEmbeddedResourceProvider
    {
        EmbeddedResource GetFile(string path);
    }
}
