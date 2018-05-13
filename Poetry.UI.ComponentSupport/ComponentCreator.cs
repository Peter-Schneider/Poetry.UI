using Poetry.UI.ReflectorSupport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.ComponentSupport
{
    public class ComponentCreator
    {
        IReflector Reflector { get; }

        public ComponentCreator(IReflector reflector)
        {
            Reflector = reflector;
        }

        public Component Create(Type type)
        {
            return null;
        }
    }
}
