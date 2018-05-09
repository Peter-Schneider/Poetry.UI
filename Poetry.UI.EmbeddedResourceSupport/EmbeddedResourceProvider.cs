using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Poetry.UI.EmbeddedResourceSupport
{
    public class EmbeddedResourceProvider : IEmbeddedResourceProvider
    {
        IEmbeddedResourcePathMatcher EmbeddedResourcePathMatcher { get; }
        public IEnumerable<EmbeddedResourceAssembly> Assemblies { get; }
        ReadOnlyDictionary<string, EmbeddedResourceAssembly> AssembliesByBasePath { get; }

        public EmbeddedResourceProvider(IEmbeddedResourcePathMatcher embeddedResourcePathMatcher, params EmbeddedResourceAssembly[] assemblies)
        {
            EmbeddedResourcePathMatcher = embeddedResourcePathMatcher;
            CheckForDuplicateBasePaths(assemblies);

            Assemblies = assemblies.Where(a => a.BasePath != string.Empty).ToList().AsReadOnly();
            AssembliesByBasePath = new ReadOnlyDictionary<string, EmbeddedResourceAssembly>(Assemblies.ToDictionary(a => a.BasePath, a => a));
        }

        void CheckForDuplicateBasePaths(IEnumerable<EmbeddedResourceAssembly> assemblies)
        {
            var set = new HashSet<string>();

            foreach(var basePath in assemblies.Select(a => a.BasePath)) {
                if (!set.Add(basePath))
                {
                    throw new Exception($"Duplicate base paths not allowed. {basePath} appeared {assemblies.Select(a => a.BasePath).Where(p => p == basePath).Count()} times");
                }
            }
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

            return assembly.EmbeddedResources.Where(r => EmbeddedResourcePathMatcher.Match(path, r)).FirstOrDefault();
        }
    }
}
