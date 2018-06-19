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
            var embeddedResourceAssemblies = new List<EmbeddedResourceAssembly>();

            embeddedResourceAssemblies.AddRange(componentRepository.GetAll().Select(c => embeddedResourceAssemblyCreator.Create(c.Id, c.Assembly)));

            EmbeddedResourceAssemblies = embeddedResourceAssemblies.AsReadOnly();
        }

        public IEnumerable<EmbeddedResourceAssembly> GetAll()
        {
            return EmbeddedResourceAssemblies;
        }
    }
}
