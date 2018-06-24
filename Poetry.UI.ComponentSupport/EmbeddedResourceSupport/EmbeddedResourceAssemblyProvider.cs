using Poetry.UI.ComponentSupport;
using Poetry.UI.ReflectionSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poetry.UI.EmbeddedResourceSupport
{
    public class EmbeddedResourceAssemblyProvider : IEmbeddedResourceAssemblyProvider
    {
        IEnumerable<EmbeddedResourceAssembly> EmbeddedResourceAssemblies { get; }

        public EmbeddedResourceAssemblyProvider(IComponentRepository componentRepository, IEmbeddedResourceAssemblyCreator embeddedResourceAssemblyCreator)
        {
            EmbeddedResourceAssemblies = componentRepository.GetAll().Select(c => embeddedResourceAssemblyCreator.Create(c.Id, c.Assembly)).ToList().AsReadOnly();
        }

        public IEnumerable<EmbeddedResourceAssembly> GetAll()
        {
            return EmbeddedResourceAssemblies;
        }
    }
}
