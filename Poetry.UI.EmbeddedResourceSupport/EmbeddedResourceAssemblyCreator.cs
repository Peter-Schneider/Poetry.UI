using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Poetry.UI.EmbeddedResourceSupport
{
    public class EmbeddedResourceAssemblyCreator : IEmbeddedResourceAssemblyCreator
    {
        public EmbeddedResourceAssembly Create(string basePath, Assembly assembly)
        {
            return new EmbeddedResourceAssembly(
                name: assembly.GetName().Name, 
                basePath: basePath,
                openEmbeddedResourceStream: r => assembly.GetManifestResourceStream(r.Name),
                embeddedResources: assembly.GetManifestResourceNames().Select(resourceName => new EmbeddedResource(resourceName)).ToArray()
            );
        }
    }
}
