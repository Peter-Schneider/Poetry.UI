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

        public EmbeddedResourceAssembly(string basePath, params EmbeddedResource[] embeddedResources)
        {
            if (string.IsNullOrEmpty(basePath))
            {
                throw new ArgumentException(nameof(basePath));
            }

            BasePath = basePath;
            EmbeddedResources = embeddedResources.ToList().AsReadOnly();
        }
    }
}
