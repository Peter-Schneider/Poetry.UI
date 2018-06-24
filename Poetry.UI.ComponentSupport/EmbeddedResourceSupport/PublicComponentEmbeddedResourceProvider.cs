using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Poetry.UI.EmbeddedResourceSupport;

namespace Poetry.UI.ComponentSupport.EmbeddedResourceSupport
{
    public class PublicComponentEmbeddedResourceProvider : IPublicComponentEmbeddedResourceProvider
    {
        IComponentRepository ComponentRepository { get; }
        IEmbeddedResourceProvider EmbeddedResourceProvider { get; }

        public PublicComponentEmbeddedResourceProvider(IComponentRepository componentRepository, IEmbeddedResourceProvider embeddedResourceProvider)
        {
            ComponentRepository = componentRepository;
            EmbeddedResourceProvider = embeddedResourceProvider;
        }

        public EmbeddedResource GetFile(string path)
        {
            if (!path.Contains('/'))
            {
                return null;
            }

            var slashIndex = path.IndexOf('/');

            var componentId = path.Substring(0, slashIndex);

            var localPath = path.Substring(slashIndex + "/".Length);

            foreach(var component in ComponentRepository.GetAll().Where(c => c.Id.Equals(componentId, StringComparison.InvariantCultureIgnoreCase)))
            {
                if (component.Scripts.Any(s => s.LocalPath.Equals(localPath, StringComparison.InvariantCultureIgnoreCase)))
                {
                    return EmbeddedResourceProvider.GetFile(path);
                }
                if (component.Styles.Any(s => s.LocalPath.Equals(localPath, StringComparison.InvariantCultureIgnoreCase)))
                {
                    return EmbeddedResourceProvider.GetFile(path);
                }
            }

            return null;
        }

        public Stream Open(EmbeddedResource embeddedResource)
        {
            return EmbeddedResourceProvider.Open(embeddedResource);
        }
    }
}
