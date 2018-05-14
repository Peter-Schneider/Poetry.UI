using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.ReflectorSupport.ReflectorAttributeSupport
{
    public class ReflectorAttributeContainer : IReflectorAttribute, IReflectorAttributeContainer
    {
        public string Name { get; }
        public object Instance { get; }

        public ReflectorAttributeContainer(string name, object instance)
        {
            Name = name;
            Instance = instance;
        }
    }
}
