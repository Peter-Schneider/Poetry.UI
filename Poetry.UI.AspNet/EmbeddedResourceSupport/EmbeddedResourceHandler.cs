﻿using Poetry.UI.AppSupport.EmbeddedResourceSupport;
using Poetry.UI.ComponentSupport.EmbeddedResourceSupport;
using Poetry.UI.EmbeddedResourceSupport;
using Poetry.UI.RoutingSupport;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Poetry.UI.AspNet.EmbeddedResourceSupport
{
    public class EmbeddedResourceHandler : IHttpHandler
    {
        IEmbeddedResourceProvider EmbeddedResourceProvider { get; set; }
        IPublicAppEmbeddedResourceProvider PublicAppEmbeddedResourceProvider { get; set; }
        IPublicComponentEmbeddedResourceProvider PublicComponentEmbeddedResourceProvider { get; set; }
        IBasePathProvider BasePathProvider { get; set; }
        string Prefix { get; set; }

        public EmbeddedResourceHandler()
        {
            EmbeddedResourceProvider = DependencyResolver.Current.GetService<IEmbeddedResourceProvider>();
            PublicAppEmbeddedResourceProvider = DependencyResolver.Current.GetService<IPublicAppEmbeddedResourceProvider>();
            PublicComponentEmbeddedResourceProvider = DependencyResolver.Current.GetService<IPublicComponentEmbeddedResourceProvider>();
            BasePathProvider = DependencyResolver.Current.GetService<IBasePathProvider>();
            Prefix = $"/{BasePathProvider.BasePath}/";
        }

        public bool IsReusable => false;

        public void ProcessRequest(HttpContext context)
        {
            var path = context.Request.Path;

            if (!path.StartsWith(Prefix, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new Exception($"This handler should only be used within the Portal basepath ({Prefix})");
            }

            path = path.Substring(Prefix.Length);

            var file = PublicAppEmbeddedResourceProvider.GetFile(path) ?? PublicComponentEmbeddedResourceProvider.GetFile(path);

            if (file == null)
            {
                throw new Exception($"This handler should only be used on an existing embedded resource");
            }

            context.Response.ContentType = MimeMapping.GetMimeMapping(Path.GetExtension(path));
            
            using(var read = EmbeddedResourceProvider.Open(file))
            {
                read.CopyTo(context.Response.OutputStream);
            }
        }
    }
}
