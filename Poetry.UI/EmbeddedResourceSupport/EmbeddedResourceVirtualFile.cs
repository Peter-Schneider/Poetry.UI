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
        string ResourceName { get; }

        public EmbeddedResourceVirtualFile(string virtualPath, string resourceName) : base(virtualPath) {
            ResourceName = resourceName;    
        }

        public override Stream Open()
        {
            var assembly = Assembly.GetExecutingAssembly();
            return assembly.GetManifestResourceStream(ResourceName);
        }
    }
}
