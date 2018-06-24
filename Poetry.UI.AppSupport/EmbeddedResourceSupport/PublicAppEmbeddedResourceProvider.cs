using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Poetry.UI.EmbeddedResourceSupport;

namespace Poetry.UI.AppSupport.EmbeddedResourceSupport
{
    public class PublicAppEmbeddedResourceProvider : IPublicAppEmbeddedResourceProvider
    {
        IAppRepository AppRepository { get; }
        IEmbeddedResourceProvider EmbeddedResourceProvider { get; }

        public PublicAppEmbeddedResourceProvider(IAppRepository appRepository, IEmbeddedResourceProvider embeddedResourceProvider)
        {
            AppRepository = appRepository;
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

            foreach(var app in AppRepository.GetAll().Where(c => c.Component.Id.Equals(componentId, StringComparison.InvariantCultureIgnoreCase)))
            {
                if (app.Scripts.Any(s => s.LocalPath.Equals(localPath, StringComparison.InvariantCultureIgnoreCase)))
                {
                    return EmbeddedResourceProvider.GetFile(path);
                }
                if (app.Styles.Any(s => s.LocalPath.Equals(localPath, StringComparison.InvariantCultureIgnoreCase)))
                {
                    return EmbeddedResourceProvider.GetFile(path);
                }
                if (app.Resources.Any(s => s.LocalPath.Equals(localPath, StringComparison.InvariantCultureIgnoreCase)))
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
