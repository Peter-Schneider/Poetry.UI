using Poetry.UI.RoutingSupport;
using System;

namespace Poetry.UI.PageEditingSupport
{
    public class PrefixProvider
    {
        IBasePathProvider BasePathProvider { get; }
        public string Prefix { get; }

        public PrefixProvider(IBasePathProvider basePathProvider)
        {
            BasePathProvider = basePathProvider;

            Prefix = $"/{BasePathProvider.BasePath}/PageEditing/";
        }
    }
}
