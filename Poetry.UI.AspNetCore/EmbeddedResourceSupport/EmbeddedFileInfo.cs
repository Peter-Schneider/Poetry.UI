using Microsoft.Extensions.FileProviders;
using Poetry.UI.EmbeddedResourceSupport;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Poetry.UI.AspNetCore.EmbeddedResourceSupport
{
    public class EmbeddedFileInfo : IFileInfo
    {
        public bool Exists { get; } = true;
        public long Length { get; }
        public string PhysicalPath { get; }
        public string Name { get; }
        public DateTimeOffset LastModified { get; }
        public bool IsDirectory { get; }

        IEmbeddedResourceProvider EmbeddedResourceProvider { get; }
        EmbeddedResource File { get; }

        public EmbeddedFileInfo(IEmbeddedResourceProvider embeddedResourceProvider, EmbeddedResource file)
        {
            EmbeddedResourceProvider = embeddedResourceProvider;
            File = file;
        }

        public Stream CreateReadStream()
        {
            return EmbeddedResourceProvider.Open(File);
        }
    }
}
