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
        EmbeddedResource EmbeddedResource { get; }

        public EmbeddedResourceVirtualFile(string virtualPath, EmbeddedResource embeddedResource) : base(virtualPath) {
            EmbeddedResource = embeddedResource;    
        }

        public override Stream Open()
        {
            return EmbeddedResource.Open();
        }
    }
}
