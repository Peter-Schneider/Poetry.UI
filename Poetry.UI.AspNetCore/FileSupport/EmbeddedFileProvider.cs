using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using Poetry.UI.EmbeddedResourceSupport;
using Poetry.UI.RoutingSupport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.AspNetCore.FileSupport
{
    public class EmbeddedFileProvider : IFileProvider
    {
        IEmbeddedResourceProvider EmbeddedResourceProvider { get; }
        string Prefix { get; }

        public EmbeddedFileProvider(IBasePathProvider basePathProvider, IEmbeddedResourceProvider embeddedResourceProvider)
        {
            EmbeddedResourceProvider = embeddedResourceProvider;
            Prefix = $"/{basePathProvider.BasePath}/";
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            return null;
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            if (!subpath.StartsWith(Prefix, StringComparison.InvariantCultureIgnoreCase))
            {
                return null;
            }

            subpath = subpath.Substring(Prefix.Length);

            var file = EmbeddedResourceProvider.GetFile(subpath);

            if (file == null)
            {
                return null;
            }

            return new EmbeddedFileInfo(EmbeddedResourceProvider, file);
        }

        public IChangeToken Watch(string filter)
        {
            return null;
        }
    }
}
