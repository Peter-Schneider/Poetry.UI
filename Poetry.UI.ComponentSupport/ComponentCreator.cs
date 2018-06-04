using Microsoft.Extensions.Logging;
using Poetry.UI.ComponentSupport.DependencySupport;
using Poetry.UI.ControllerSupport;
using Poetry.UI.ReflectionSupport;
using Poetry.UI.ScriptSupport;
using Poetry.UI.StyleSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Poetry.UI.ComponentSupport
{
    public class ComponentCreator : IComponentCreator
    {
        ILogger<ComponentCreator> Logger { get; }
        IComponentTypeProvider ComponentTypeProvider { get; }
        IComponentDependencyCreator ComponentDependencyCreator { get; }
        IComponentControllerCreator ComponentControllerCreator { get; }
        IScriptCreator ScriptCreator { get; }
        IStyleCreator StyleCreator { get; }

        public ComponentCreator(ILogger<ComponentCreator> logger, IComponentTypeProvider componentTypeProvider, IComponentDependencyCreator componentDependencyCreator, IComponentControllerCreator componentControllerCreator, IScriptCreator scriptCreator, IStyleCreator styleCreator)
        {
            Logger = logger;
            ComponentTypeProvider = componentTypeProvider;
            ComponentDependencyCreator = componentDependencyCreator;
            ComponentControllerCreator = componentControllerCreator;
            ScriptCreator = scriptCreator;
            StyleCreator = styleCreator;
        }

        public IEnumerable<Component> Create()
        {
            var result = new List<Component>();

            foreach (var type in ComponentTypeProvider.GetTypes())
            {
                var attribute = type.GetCustomAttribute<ComponentAttribute>();

                if (attribute == null)
                {
                    continue;
                }

                result.Add(new Component(attribute.Id, new AssemblyWrapper(type.Assembly), ComponentDependencyCreator.Create(type), ComponentControllerCreator.Create(type), ScriptCreator.Create(type), StyleCreator.Create(type)));
            }

            if (Logger.IsEnabled(LogLevel.Information))
            {
                Logger.LogInformation($"Detected {result.Count} components: {string.Join(", ", result.Select(r => r.Id))}");
            }

            return result;
        }
    }
}
