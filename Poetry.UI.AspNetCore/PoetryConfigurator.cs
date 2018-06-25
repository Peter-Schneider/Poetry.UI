using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Poetry.UI.AppSupport;
using Poetry.UI.AspNetCore.DependencyInjectionSupport;
using Poetry.UI.AspNetCore.EmbeddedResourceSupport;
using Poetry.UI.AspNetCore.LoggerSupport;
using Poetry.UI.AspNetCore.PageEditingSupport;
using Poetry.UI.BladeSupport;
using Poetry.UI.ComponentSupport;
using Poetry.UI.ComponentSupport.InitializerSupport;
using Poetry.UI.ContextMenu;
using Poetry.UI.ControllerSupport;
using Poetry.UI.DependencyInjectionSupport;
using Poetry.UI.EmbeddedResourceSupport;
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
    public class PoetryConfigurator
    {
        IServiceCollection ServiceCollection { get; }
        string BasePath { get; set; } = "Admin";
        List<AssemblyWrapper> Assemblies { get; } = new List<AssemblyWrapper>
        {
            new AssemblyWrapper(typeof(CoreComponent).Assembly),
            new AssemblyWrapper(typeof(PortalComponent).Assembly),
            new AssemblyWrapper(typeof(BladeComponent).Assembly),
            new AssemblyWrapper(typeof(TranslationComponent).Assembly),
            new AssemblyWrapper(typeof(DataTableComponent).Assembly),
            new AssemblyWrapper(typeof(FormComponent).Assembly),
            new AssemblyWrapper(typeof(ContextMenuComponent).Assembly),
            new AssemblyWrapper(typeof(PageEditingComponent).Assembly),
        };
        List<Action<IContainer>> ContainerOverrides { get; } = new List<Action<IContainer>>();

        public PoetryConfigurator(IServiceCollection serviceCollection)
        {
            ServiceCollection = serviceCollection;
        }

        public PoetryConfigurator WithBasePath(string basePath)
        {
            BasePath = basePath.Trim('/');
            return this;
        }

        public PoetryConfigurator AddAssembly(Assembly assembly)
        {
            Assemblies.Add(new AssemblyWrapper(assembly));
            return this;
        }

        public PoetryConfigurator InjectType<T1, T2>() where T1 : class where T2 : class, T1
        {
            ContainerOverrides.Add(c => c.RegisterType<T1, T2>());

            return this;
        }

        public PoetryConfigurator InjectSingleton<T1, T2>() where T1 : class where T2 : class, T1
        {
            ContainerOverrides.Add(c => c.RegisterSingleton<T1, T2>());

            return this;
        }

        public PoetryConfigurator InjectSingleton<T>(T instance) where T : class
        {
            ContainerOverrides.Add(c => c.RegisterSingleton(instance));

            return this;
        }

        public void Done()
        {
            var poetryContainer = new Container(ServiceCollection);

            poetryContainer.RegisterType(typeof(ILogger<>), typeof(DefaultLogger<>));
            poetryContainer.RegisterSingleton<IInstantiator, Instantiator>();
            poetryContainer.RegisterSingleton<IBasePathProvider>(new BasePathProvider(BasePath));
            poetryContainer.RegisterSingleton<IAssemblyProvider>(new AssemblyProvider(Assemblies));

            poetryContainer.RegisterSingleton<IModeProvider, ModeProvider>();

            new ComponentSupportDependencyInjector().InjectDependencies(poetryContainer);
            new DependencyInjectionSupportDependencyInjector().InjectDependencies(poetryContainer);
            new ControllerSupportDependencyInjector().InjectDependencies(poetryContainer);
            new EmbeddedResourceSupportDependencyInjector().InjectDependencies(poetryContainer);
            new AppSupportDependencyInjector().InjectDependencies(poetryContainer);

            var serviceProvider = ServiceCollection.BuildServiceProvider();

            foreach (var injector in serviceProvider.GetService<IDependencyInjectorProvider>().GetAll())
            {
                injector.InjectDependencies(poetryContainer);
            }

            foreach (var containerOverride in ContainerOverrides)
            {
                containerOverride(poetryContainer);
            }

            serviceProvider = ServiceCollection.BuildServiceProvider();

            var t = serviceProvider.GetService<Microsoft.Extensions.FileProviders.IFileProvider>();

            var instantiator = serviceProvider.GetService<IInstantiator>();

            ServiceCollection.Configure<RazorViewEngineOptions>(opts =>
                opts.FileProviders.Add((EmbeddedFileProvider)instantiator.Instantiate(typeof(EmbeddedFileProvider)))
            );
        }
    }
}
