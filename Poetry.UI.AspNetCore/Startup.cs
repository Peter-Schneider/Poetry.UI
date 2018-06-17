using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Poetry.UI.AppSupport;
using Poetry.UI.AspNetCore.DependencyInjectionSupport;
using Poetry.UI.AspNetCore.FileSupport;
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
        public static void AddPoetryUI(this IServiceCollection services)
        {
            var BasePath = "Admin";
            var Assemblies = new List<AssemblyWrapper>
            {
                new AssemblyWrapper(Assembly.GetCallingAssembly()),
                new AssemblyWrapper(typeof(Startup).Assembly),
                new AssemblyWrapper(typeof(ContextMenuComponent).Assembly),
                new AssemblyWrapper(typeof(FormComponent).Assembly),
                new AssemblyWrapper(typeof(DataTableComponent).Assembly),
                new AssemblyWrapper(typeof(PageEditingComponent).Assembly),
                new AssemblyWrapper(typeof(TranslationComponent).Assembly),
                new AssemblyWrapper(typeof(PortalComponent).Assembly),
            };

            var poetryContainer = new Container(services);

            poetryContainer.RegisterType(typeof(ILogger<>), typeof(DefaultLogger<>));
            poetryContainer.RegisterType<IInstantiator, Instantiator>();
            poetryContainer.RegisterSingleton<IBasePathProvider>(new BasePathProvider(BasePath));
            poetryContainer.RegisterSingleton<IAssemblyProvider>(new AssemblyProvider(Assemblies));

            poetryContainer.RegisterType<IFileProvider, FileProvider>();
            poetryContainer.RegisterType<IModeProvider, ModeProvider>();

            new ScriptSupportDependencyInjector().InjectDependencies(poetryContainer);
            new StyleSupportDependencyInjector().InjectDependencies(poetryContainer);
            new ComponentSupportDependencyInjector().InjectDependencies(poetryContainer);
            new DependencyInjectionSupportDependencyInjector().InjectDependencies(poetryContainer);
            new ControllerSupportDependencyInjector().InjectDependencies(poetryContainer);
            new EmbeddedResourceSupportDependencyInjector().InjectDependencies(poetryContainer);
            new RoutingSupportDependencyInjector().InjectDependencies(poetryContainer);
            new AppSupportDependencyInjector().InjectDependencies(poetryContainer);

            var serviceProvider = services.BuildServiceProvider();

            foreach (var injector in serviceProvider.GetService<IDependencyInjectorProvider>().GetAll())
            {
                injector.InjectDependencies(poetryContainer);
            }

            //foreach (var containerOverride in ContainerOverrides)
            //{
            //    containerOverride(poetryContainer);
            //}

            var t = serviceProvider.GetService<Microsoft.Extensions.FileProviders.IFileProvider>();

            var instantiator = new Instantiator(serviceProvider);
            
            services.Configure<RazorViewEngineOptions>(opts =>
                opts.FileProviders.Add((EmbeddedFileProvider)instantiator.Instantiate(typeof(EmbeddedFileProvider)))
            );
        }

        public static void UsePoetryUI(this IApplicationBuilder app)
        {
            app.UseMiddleware<StaticFilesMiddleware>();
            app.UseMiddleware<ControllerMiddleware>();
            app.UseMiddleware<PageEditingMiddleware>();
        }
    }
}
