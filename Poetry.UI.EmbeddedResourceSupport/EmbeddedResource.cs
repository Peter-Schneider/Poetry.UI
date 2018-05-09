using System;
using System.IO;

namespace Poetry.UI.EmbeddedResourceSupport
{
    public class EmbeddedResource
    {
        public string Name { get; }
        Func<Stream> OpenFunc { get; }

        public EmbeddedResource(string name, Func<Stream> openFunc)
        {
            Name = name;
            OpenFunc = openFunc;
        }

        public Stream Open()
        {
            return OpenFunc();
        }
    }
}