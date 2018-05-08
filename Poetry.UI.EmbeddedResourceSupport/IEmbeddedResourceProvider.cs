using System.Collections.Generic;

namespace Poetry.UI.EmbeddedResourceSupport
{
    public interface IEmbeddedResourceProvider
    {
        IEnumerable<EmbeddedResourceAssembly> Assemblies { get; }
        EmbeddedResource GetFile(string path);
    }
}