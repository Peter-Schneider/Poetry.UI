using Poetry.UI.ComponentSupport.DependencySupport;
using Poetry.UI.ControllerSupport;
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
        IComponentTypeProvider ComponentTypeProvider { get; }
        IComponentDependencyCreator ComponentDependencyCreator { get; }
        IComponentControllerCreator ComponentControllerCreator { get; }
        IScriptCreator ScriptCreator { get; }
        IStyleCreator StyleCreator { get; }

        public ComponentCreator(IComponentTypeProvider componentTypeProvider, IComponentDependencyCreator componentDependencyCreator, IComponentControllerCreator componentControllerCreator, IScriptCreator scriptCreator, IStyleCreator styleCreator)
        {
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

                result.Add(new Component(attribute.Id, type.Assembly, ComponentDependencyCreator.Create(type), ComponentControllerCreator.Create(type), ScriptCreator.Create(type), StyleCreator.Create(type)));
            }

            return result;
        }
    }
}
