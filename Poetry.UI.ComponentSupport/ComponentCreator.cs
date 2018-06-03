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
        IComponentDependencyCreator ComponentDependencyCreator { get; }
        IComponentControllerCreator ComponentControllerCreator { get; }
        IScriptCreator ScriptCreator { get; }
        IStyleCreator StyleCreator { get; }

        public ComponentCreator(IComponentDependencyCreator componentDependencyCreator, IComponentControllerCreator componentControllerCreator, IScriptCreator scriptCreator, IStyleCreator styleCreator)
        {
            ComponentDependencyCreator = componentDependencyCreator;
            ComponentControllerCreator = componentControllerCreator;
            ScriptCreator = scriptCreator;
            StyleCreator = styleCreator;
        }

        public Component Create(Type type)
        {
            if(type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            var attribute = type.GetCustomAttribute<ComponentAttribute>();

            if(attribute == null)
            {
                throw new TypeIsMissingComponentAttributeException(type);
            }

            return new Component(attribute.Id, type.Assembly, ComponentDependencyCreator.Create(type), ComponentControllerCreator.Create(type), ScriptCreator.Create(type), StyleCreator.Create(type));
        }
    }
}
