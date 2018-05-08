using log4net;
using Poetry.UI.MvcSupport;
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
    public class EmbeddedResourceVirtualPathProvider : VirtualPathProvider
    {
        ILog Log { get; } = LogManager.GetLogger(typeof(EmbeddedResourceVirtualPathProvider));
        IBasePathProvider BasePathProvider { get; }
        IEmbeddedResourceProvider EmbeddedResourceProvider { get; }

        public EmbeddedResourceVirtualPathProvider(IBasePathProvider basePathProvider, IEmbeddedResourceProvider embeddedResourceProvider) {
            BasePathProvider = basePathProvider;
            EmbeddedResourceProvider = embeddedResourceProvider;

            if (Log.IsDebugEnabled)
            {
                Log.Debug($"Embedded resources found:\n{string.Join("\n", embeddedResourceProvider.Assemblies.Select(a => a.EmbeddedResources.Select(r => $"{basePathProvider.BasePath}/{a.BasePath}/{r.Path}")))}");
            }
        }

        public override bool FileExists(string virtualPath)
        {
            var path = virtualPath;

            if (path.StartsWith("~/"))
            {
                path = path.Substring("~/".Length);
            }

            if (path.StartsWith("/"))
            {
                path = path.Substring("/".Length);
            }

            var prefix = $"{BasePathProvider.BasePath}/";

            if (!path.StartsWith(prefix))
            {
                return Previous.FileExists(virtualPath);
            }

            path = path.Substring(prefix.Length);

            if (!Exists(path))
            {
                if (Log.IsDebugEnabled)
                {
                    Log.Debug($"File {path} did not exist, exiting");
                }

                return Previous.FileExists(virtualPath);
            }
            
            return true;
        }
        
        public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            var path = virtualPath;

            if (path.StartsWith("~/"))
            {
                path = path.Substring("~/".Length);
            }

            if (path.StartsWith("/"))
            {
                path = path.Substring("/".Length);
            }

            var prefix = $"{BasePathProvider.BasePath}/";

            if (!path.StartsWith(prefix))
            {
                return Previous.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
            }

            path = path.Substring(prefix.Length);

            if (!Exists(path))
            {
                if (Log.IsDebugEnabled)
                {
                    Log.Debug($"Resource {path} did not exist, exiting");
                }

                return Previous.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
            }

            return null;
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            var path = virtualPath;

            if (path.StartsWith("~/"))
            {
                path = path.Substring("~/".Length);
            }

            if (path.StartsWith("/"))
            {
                path = path.Substring("/".Length);
            }

            var prefix = $"{BasePathProvider.BasePath}/";

            if (!path.StartsWith(prefix))
            {
                //if (Log.IsDebugEnabled)
                //{
                //    Log.Debug($"Path {path} did not start with {prefix}, exiting");
                //}

                return Previous.GetFile(virtualPath);
            }

            path = path.Substring(prefix.Length);

            if (!Exists(path))
            {
                if (Log.IsDebugEnabled)
                {
                    Log.Debug($"Resource {path} did not exist, exiting");
                }

                return Previous.GetFile(virtualPath);
            }

            return new EmbeddedResourceVirtualFile(virtualPath, EmbeddedResourceProvider.GetFile(path));
        }

        bool Exists(string path)
        {
            return EmbeddedResourceProvider.GetFile(path) != null;
        }
    }
}
