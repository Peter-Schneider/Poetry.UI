using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poetry.UI.EmbeddedResourceSupport
{
    public class EmbeddedResourceAssembly
    {
        public string BasePath { get; }
        public IEnumerable<EmbeddedResource> EmbeddedResources { get; }
        ReadOnlyDictionary<string, EmbeddedResource> EmbeddedResourcesByPath { get; }

        public EmbeddedResourceAssembly(string basePath, params EmbeddedResource[] embeddedResources)
        {
            BasePath = basePath;
            EmbeddedResources = embeddedResources;
            EmbeddedResourcesByPath = new ReadOnlyDictionary<string, EmbeddedResource>(embeddedResources.ToDictionary(file => file.Path, file => file));
        }

        public EmbeddedResource GetFile(string path)
        {
            return EmbeddedResourcesByPath.ContainsKey(path) ? EmbeddedResourcesByPath[path] : null;
        }
    }
}
