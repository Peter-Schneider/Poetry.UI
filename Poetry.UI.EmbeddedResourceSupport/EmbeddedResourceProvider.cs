using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Poetry.UI.EmbeddedResourceSupport
{
    public class EmbeddedResourceProvider : IEmbeddedResourceProvider
    {
        public IEnumerable<EmbeddedResourceAssembly> Assemblies { get; }
        ReadOnlyDictionary<string, EmbeddedResourceAssembly> AssembliesByBasePath { get; }

        public EmbeddedResourceProvider(params EmbeddedResourceAssembly[] assemblies)
        {
            Assemblies = assemblies.ToList().AsReadOnly();
            AssembliesByBasePath = new ReadOnlyDictionary<string, EmbeddedResourceAssembly>(Assemblies.ToDictionary(a => a.BasePath, a => a));
        }

        public EmbeddedResource GetFile(string path)
        {
            if (!path.Contains('/'))
            {
                return null;
            }

            var slashIndex = path.IndexOf('/');
            var basePath = path.Substring(0, slashIndex);

            var assembly = Assemblies.Where(a => a.BasePath == basePath).FirstOrDefault();

            if(assembly == null)
            {
                return null;
            }

            path = path.Substring(slashIndex + 1);

            return assembly.GetFile(path);
        }
    }
}
