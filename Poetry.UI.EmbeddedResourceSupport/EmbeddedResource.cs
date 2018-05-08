using System;
using System.IO;

namespace Poetry.UI.EmbeddedResourceSupport
{
    public class EmbeddedResource
    {
        public string Path { get; }
        Func<Stream> OpenFunc { get; }

        public EmbeddedResource(string path, Func<Stream> openFunc)
        {
            Path = path;
            OpenFunc = openFunc;
        }

        public Stream Open()
        {
            return OpenFunc();
        }
    }
}