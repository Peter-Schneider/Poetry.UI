using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Poetry.UI.DataTableSupport.BackendSupport
{
    public class BackendProvider : IBackendProvider
    {
        IDictionary<string, IBackend> Backends { get; }

        public BackendProvider(IBackendCreator backendCreator)
        {
            Backends = new ReadOnlyDictionary<string, IBackend>(new Dictionary<string, IBackend>(backendCreator.Create()));
        }

        public IBackend GetFor(string id)
        {
            if (!Backends.ContainsKey(id))
            {
                return null;
            }

            return Backends[id];
        }
    }
}
