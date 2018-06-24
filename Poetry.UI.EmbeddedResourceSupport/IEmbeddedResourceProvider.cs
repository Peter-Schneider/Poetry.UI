using System.Collections.Generic;
using System.IO;

namespace Poetry.UI.EmbeddedResourceSupport
{
    public interface IEmbeddedResourceProvider
    {
        EmbeddedResource GetFile(string path);
        Stream Open(EmbeddedResource embeddedResource);
    }
}