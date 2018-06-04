using Poetry.UI.ReflectionSupport;
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
        public EmbeddedResourceAssembly Create(string basePath, AssemblyWrapper assembly)
        {
            return new EmbeddedResourceAssembly(
                name: assembly.Assembly.GetName().Name, 
                basePath: basePath,
                openEmbeddedResourceStream: r => assembly.Assembly.GetManifestResourceStream(r.Name),
                embeddedResources: assembly.Assembly.GetManifestResourceNames().Select(resourceName => new EmbeddedResource(resourceName)).ToArray()
            );
        }
    }
}
