using Microsoft.AspNetCore.Http;
using Poetry.UI.AppSupport.EmbeddedResourceSupport;
using Poetry.UI.ComponentSupport.EmbeddedResourceSupport;
using Poetry.UI.EmbeddedResourceSupport;
using Poetry.UI.RoutingSupport;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Poetry.UI.AspNetCore.EmbeddedResourceSupport
{
    public class StaticFilesMiddleware
    {
        IPublicAppEmbeddedResourceProvider PublicAppEmbeddedResourceProvider { get; }
        IPublicComponentEmbeddedResourceProvider PublicComponentEmbeddedResourceProvider { get; }
        IEmbeddedResourceProvider EmbeddedResourceProvider { get; }
        string Prefix { get; }
        RequestDelegate Next { get; }

        public StaticFilesMiddleware(IBasePathProvider basePathProvider, IPublicAppEmbeddedResourceProvider publicAppEmbeddedResourceProvider, IPublicComponentEmbeddedResourceProvider publicComponentEmbeddedResourceProvider, IEmbeddedResourceProvider embeddedResourceProvider, RequestDelegate next)
        {
            PublicAppEmbeddedResourceProvider = publicAppEmbeddedResourceProvider;
            PublicComponentEmbeddedResourceProvider = publicComponentEmbeddedResourceProvider;
            EmbeddedResourceProvider = embeddedResourceProvider;
            Prefix = $"/{basePathProvider.BasePath}/";
            Next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var path = httpContext.Request.Path.ToString();

            if (!path.StartsWith(Prefix, StringComparison.InvariantCultureIgnoreCase))
            {
                await Next(httpContext);
                return;
            }

            path = path.Substring(Prefix.Length);

            var file = PublicAppEmbeddedResourceProvider.GetFile(path) ?? PublicComponentEmbeddedResourceProvider.GetFile(path);

            if(file == null)
            {
                await Next(httpContext);
                return;
            }

            if (path.EndsWith(".js"))
            {
                httpContext.Response.ContentType = "application/javascript";
            }

            using(var read = EmbeddedResourceProvider.Open(file))
            {
                await read.CopyToAsync(httpContext.Response.Body);
            }
        }
    }
}
