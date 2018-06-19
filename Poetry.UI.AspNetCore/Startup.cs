using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Poetry.UI.AppSupport;
using Poetry.UI.AspNetCore.DependencyInjectionSupport;
using Poetry.UI.AspNetCore.EmbeddedResourceSupport;
using Poetry.UI.AspNetCore.LoggerSupport;
using Poetry.UI.AspNetCore.PageEditingSupport;
using Poetry.UI.AspNetCore.RoutingSupport;
using Poetry.UI.ComponentSupport;
using Poetry.UI.ContextMenu;
using Poetry.UI.ControllerSupport;
using Poetry.UI.DependencyInjectionSupport;
using Poetry.UI.EmbeddedResourceSupport;
using Poetry.UI.FileSupport;
using Poetry.UI.FormSupport;
using Poetry.UI.PageEditingSupport;
using Poetry.UI.PortalSupport;
using Poetry.UI.ReflectionSupport;
using Poetry.UI.RoutingSupport;
using Poetry.UI.ScriptSupport;
using Poetry.UI.StyleSupport;
using Poetry.UI.TableSupport;
using Poetry.UI.TranslationSupport;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Poetry.UI.AspNetCore
{
    public static class Startup
    {
        public static PoetryConfigurator AddPoetryUI(this IServiceCollection serviceCollection)
        {
            var configurator = new PoetryConfigurator(serviceCollection);

            configurator.AddAssembly(Assembly.GetCallingAssembly());

            return configurator;
        }

        public static void UsePoetryUI(this IApplicationBuilder app)
        {
            app.UseMiddleware<StaticFilesMiddleware>();
            app.UseMiddleware<ControllerMiddleware>();
            app.UseMiddleware<PageEditingMiddleware>();
        }
    }
}
