using Microsoft.AspNetCore.Http;
using Poetry.UI.PageEditingSupport;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Poetry.UI.AspNetCore.PageEditingSupport
{
    public class PageEditingMiddleware
    {
        IModeProvider ModeProvider { get; }
        IPathPrefixProvider PathPrefixProvider { get; }
        RequestDelegate Next { get; }

        public PageEditingMiddleware(IModeProvider modeProvider, IPathPrefixProvider pathPrefixProvider, RequestDelegate next)
        {
            ModeProvider = modeProvider;
            PathPrefixProvider = pathPrefixProvider;
            Next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var path = httpContext.Request.Path.ToString();

            if (path.StartsWith(PathPrefixProvider.Prefix, StringComparison.InvariantCultureIgnoreCase))
            {
                ModeProvider.SetIsPageEditing(httpContext, true);

                httpContext.Request.Path = "/" + path.Substring(PathPrefixProvider.Prefix.Length);
            }

            await Next(httpContext);
        }
    }
}
