using Poetry.UI.RoutingSupport;
using System;

namespace Poetry.UI.PageEditingSupport
{
    public class PathPrefixProvider
    {
        IBasePathProvider BasePathProvider { get; }
        public string Prefix { get; }

        public PathPrefixProvider(IBasePathProvider basePathProvider)
        {
            BasePathProvider = basePathProvider;

            Prefix = $"/{BasePathProvider.BasePath}/PageEditing/EditPage/";
        }
    }
}
