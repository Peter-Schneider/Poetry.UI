using Poetry.UI.ControllerSupport;
using Poetry.UI.ScriptSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Poetry.UI.ComponentSupport
{
    public class ComponentCreator
    {
        IComponentControllerCreator ComponentControllerCreator { get; }
        IScriptCreator ScriptCreator { get; }

        public ComponentCreator(IComponentControllerCreator componentControllerCreator, IScriptCreator scriptCreator)
        {
            ComponentControllerCreator = componentControllerCreator;
            ScriptCreator = scriptCreator;
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

            return new Component(attribute.Id, type.Assembly, ComponentControllerCreator.Create(type), ScriptCreator.Create(type));
        }
    }
}
