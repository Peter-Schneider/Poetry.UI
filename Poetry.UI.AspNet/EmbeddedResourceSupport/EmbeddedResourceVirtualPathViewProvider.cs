using Poetry.UI.RoutingSupport;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;

namespace Poetry.UI.EmbeddedResourceSupport
{
    public class EmbeddedResourceVirtualPathViewProvider : VirtualPathProvider
    {
        IBasePathProvider BasePathProvider { get; }
        string Prefix { get; }
        string Suffix { get; }
        IEmbeddedResourceProvider EmbeddedResourceProvider { get; }

        public EmbeddedResourceVirtualPathViewProvider(IBasePathProvider basePathProvider, IEmbeddedResourceProvider embeddedResourceProvider) {
            BasePathProvider = basePathProvider;
            Prefix = $"/{BasePathProvider.BasePath}/";
            Suffix = ".cshtml";
            EmbeddedResourceProvider = embeddedResourceProvider;
        }

        public override bool FileExists(string virtualPath)
        {
            var path = virtualPath;

            if (path.StartsWith("~"))
            {
                path = path.Substring("~".Length);
            }

            if (!path.StartsWith(Prefix, StringComparison.InvariantCultureIgnoreCase) || !path.EndsWith(Suffix, StringComparison.InvariantCultureIgnoreCase))
            {
                return Previous.FileExists(virtualPath);
            }

            path = path.Substring(Prefix.Length);

            if (!Exists(path))
            {
                return Previous.FileExists(virtualPath);
            }
            
            return true;
        }
        
        public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            var path = virtualPath;

            if (path.StartsWith("~"))
            {
                path = path.Substring("~".Length);
            }

            if (!path.StartsWith(Prefix, StringComparison.InvariantCultureIgnoreCase) || !path.EndsWith(Suffix, StringComparison.InvariantCultureIgnoreCase))
            {
                return Previous.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
            }

            path = path.Substring(Prefix.Length);

            if (!Exists(path))
            {
                return Previous.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
            }

            return null;
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            var path = virtualPath;

            if (path.StartsWith("~"))
            {
                path = path.Substring("~".Length);
            }

            if (!path.StartsWith(Prefix, StringComparison.InvariantCultureIgnoreCase) || !path.EndsWith(Suffix, StringComparison.InvariantCultureIgnoreCase))
            {
                return Previous.GetFile(virtualPath);
            }

            path = path.Substring(Prefix.Length);

            if (!Exists(path))
            {
                return Previous.GetFile(virtualPath);
            }

            return new EmbeddedResourceVirtualFile(EmbeddedResourceProvider, EmbeddedResourceProvider.GetFile(path), virtualPath);
        }

        bool Exists(string path)
        {
            return EmbeddedResourceProvider.GetFile(path) != null;
        }
    }
}
