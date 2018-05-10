using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace Poetry.UI.EmbeddedResourceSupport
{
    public class EmbeddedResourceVirtualFile : VirtualFile
    {
        IEmbeddedResourceProvider EmbeddedResourceProvider { get; }
        EmbeddedResource EmbeddedResource { get; }

        public EmbeddedResourceVirtualFile(IEmbeddedResourceProvider embeddedResourceProvider, EmbeddedResource embeddedResource, string virtualPath) : base(virtualPath) {
            EmbeddedResourceProvider = embeddedResourceProvider;
            EmbeddedResource = embeddedResource;    
        }

        public override Stream Open()
        {
            return EmbeddedResourceProvider.Open(EmbeddedResource);
        }
    }
}
