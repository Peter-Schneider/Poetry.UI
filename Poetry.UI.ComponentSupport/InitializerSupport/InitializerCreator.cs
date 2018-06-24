using Poetry.UI.DependencyInjectionSupport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.ComponentSupport.InitializerSupport
{
    public class InitializerCreator : IInitializerCreator
    {
        IInstantiator Instantiator { get; }

        public InitializerCreator(IInstantiator instantiator)
        {
            Instantiator = instantiator;
        }

        public IInitializer Create(Type type)
        {
            return (IInitializer)Instantiator.Instantiate(type);
        }
    }
}
