using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poetry.UI.ComponentSupport.InitializerSupport
{
    public class InitializerProvider : IInitializerProvider
    {
        IEnumerable<IInitializer> Instances { get; }

        public InitializerProvider(IInitializerTypeProvider initializerTypeProvider, IInitializerCreator initializerCreator)
        {
            Instances = initializerTypeProvider.GetAll().Select(t => initializerCreator.Create(t)).ToList().AsReadOnly();
        }

        public IEnumerable<IInitializer> GetAll()
        {
            return Instances;
        }
    }
}
