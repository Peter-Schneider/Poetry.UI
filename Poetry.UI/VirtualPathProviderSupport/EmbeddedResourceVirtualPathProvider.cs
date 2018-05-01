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

namespace Poetry.UI.VirtualPathProviderSupport
{
    public class EmbeddedResourceVirtualPathProvider : VirtualPathProvider
    {
        Assembly Assembly { get; } = Assembly.GetExecutingAssembly();
        ILog Log { get; } = LogManager.GetLogger(typeof(EmbeddedResourceVirtualPathProvider));
        IBasePathProvider BasePathProvider { get; }

        public EmbeddedResourceVirtualPathProvider(IBasePathProvider basePathProvider) {
            BasePathProvider = basePathProvider;

            if (Log.IsDebugEnabled)
            {
                Log.Debug($"Embedded resources found:\n{string.Join("\n", Assembly.GetManifestResourceNames())}");
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
                //if (Log.IsDebugEnabled)
                //{
                //    Log.Debug($"Path {path} did not start with {prefix}, exiting");
                //}

                return Previous.FileExists(virtualPath);
            }

            path = path.Substring(prefix.Length);

            var name = GetResourceName(path);

            if (!Exists(name))
            {
                if (Log.IsDebugEnabled)
                {
                    Log.Debug($"File {name} did not exist, exiting");
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

            var name = GetResourceName(path);

            if (!Exists(name))
            {
                if (Log.IsDebugEnabled)
                {
                    Log.Debug($"Resource {name} did not exist, exiting");
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

            var name = GetResourceName(path);

            if (!Exists(name))
            {
                if (Log.IsDebugEnabled)
                {
                    Log.Debug($"Resource {name} did not exist, exiting");
                }

                return Previous.GetFile(virtualPath);
            }

            return new EmbeddedResourceVirtualFile(virtualPath, name);
        }

        bool Exists(string resourceName)
        {
            using (var stream = Assembly.GetManifestResourceStream(resourceName))
            {
                return stream != null;
            }
        }

        string GetResourceName(string virtualPath)
        {
            var segments = virtualPath.Split('/');
            var filename = segments.Last();
            var path = string.Join("/", segments.Reverse().Skip(1).Reverse()).Replace('/', '.').Replace('-', '_');

            if(path != string.Empty)
            {
                path += ".";
            }

            var resourceName = $"{Assembly.GetName().Name}.{path}{filename}";

            return resourceName;
        }
    }
}
