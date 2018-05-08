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
        Regex PathSegmentPattern = new Regex(@"[^.]+\.");

        public EmbeddedResourceAssembly Create(string basePath, Assembly assembly)
        {
            var resourceNames = assembly.GetManifestResourceNames();
            var assemblyName = assembly.GetName().Name;

            return new EmbeddedResourceAssembly(basePath, resourceNames.Select(resourceName => {
                var path = resourceName.Substring(assemblyName.Length + ".".Length);

                path = PathSegmentPattern.Replace(path, m => m.Value.Any(char.IsUpper) ? m.Value.Substring(0, m.Value.Length - 1) + "/" : m.Value);

                return new EmbeddedResource(path, () => assembly.GetManifestResourceStream(resourceName));
            }).ToArray());
        }
    }
}
