using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poetry.UI.EmbeddedResourceSupport
{
    public class EmbeddedResourceAssembly
    {
        public string Name { get; }
        public string BasePath { get; }
        Func<EmbeddedResource, Stream> OpenEmbeddedResourceStream { get; }
        public IEnumerable<EmbeddedResource> EmbeddedResources { get; }
        ISet<EmbeddedResource> EmbeddedResourcesSet { get; }

        public EmbeddedResourceAssembly(string name, string basePath, Func<EmbeddedResource, Stream> openEmbeddedResourceStream, params EmbeddedResource[] embeddedResources)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException(nameof(name));
            }
            if (string.IsNullOrEmpty(basePath))
            {
                throw new ArgumentException(nameof(basePath));
            }

            Name = name;
            BasePath = basePath;
            OpenEmbeddedResourceStream = openEmbeddedResourceStream;
            EmbeddedResources = embeddedResources.ToList().AsReadOnly();
            EmbeddedResourcesSet = new HashSet<EmbeddedResource>(embeddedResources);
        }

        public bool Contains(EmbeddedResource embeddedResource)
        {
            return EmbeddedResources.Contains(embeddedResource);
        }

        public Stream Open(EmbeddedResource embeddedResource)
        {
            return OpenEmbeddedResourceStream(embeddedResource);
        }
    }
}
