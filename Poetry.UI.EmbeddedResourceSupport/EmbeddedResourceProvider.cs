using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace Poetry.UI.EmbeddedResourceSupport
{
    public class EmbeddedResourceProvider : IEmbeddedResourceProvider
    {
        IEmbeddedResourcePathMatcher EmbeddedResourcePathMatcher { get; }
        public IEnumerable<EmbeddedResourceAssembly> Assemblies { get; }

        public EmbeddedResourceProvider(IEmbeddedResourcePathMatcher embeddedResourcePathMatcher, IEmbeddedResourceAssemblyProvider embeddedResourceAssemblyProvider)
        {
            EmbeddedResourcePathMatcher = embeddedResourcePathMatcher;

            Assemblies = embeddedResourceAssemblyProvider.GetAll().Where(a => a.BasePath != string.Empty).ToList().AsReadOnly();
        }

        public EmbeddedResource GetFile(string path)
        {
            if (!path.Contains('/'))
            {
                return null;
            }

            var slashIndex = path.IndexOf('/');
            var basePath = path.Substring(0, slashIndex);

            path = path.Substring(slashIndex + 1);

            foreach (var assembly in Assemblies.Where(a => a.BasePath == basePath))
            {
                var result = assembly.EmbeddedResources.Where(r => EmbeddedResourcePathMatcher.Match(assembly, r, path)).FirstOrDefault();

                if(result != null)
                {
                    return result;
                }
            }

            return null;
        }

        public Stream Open(EmbeddedResource embeddedResource)
        {
            var assembly = Assemblies.Where(a => a.Contains(embeddedResource)).Single();

            return assembly.Open(embeddedResource);
        }
    }
}
