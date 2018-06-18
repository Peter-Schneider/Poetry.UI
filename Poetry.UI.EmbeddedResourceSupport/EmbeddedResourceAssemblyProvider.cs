using Poetry.UI.AppSupport;
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

        public EmbeddedResourceAssemblyProvider(IComponentRepository componentRepository, IAppRepository appRepository, IEmbeddedResourceAssemblyCreator embeddedResourceAssemblyCreator)
        {
            var embeddedResourceAssemblies = new List<EmbeddedResourceAssembly>();

            embeddedResourceAssemblies.AddRange(componentRepository.GetAll().Select(c => embeddedResourceAssemblyCreator.Create(c.Id, c.Assembly)));
            embeddedResourceAssemblies.AddRange(appRepository.GetAll().Select(c => embeddedResourceAssemblyCreator.Create(c.Name, c.Assembly)));

            EmbeddedResourceAssemblies = embeddedResourceAssemblies.AsReadOnly();
        }

        public IEnumerable<EmbeddedResourceAssembly> GetAll()
        {
            return EmbeddedResourceAssemblies;
        }
    }
}
