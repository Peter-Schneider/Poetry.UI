using Microsoft.AspNetCore.Hosting;
using Poetry.UI.FileSupport;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Poetry.UI.AspNetCore.EmbeddedResourceSupport
{
    public class FileProvider : IFileProvider
    {
        IHostingEnvironment HostingEnvironment { get; }

        public FileProvider(IHostingEnvironment hostingEnvironment)
        {
            HostingEnvironment = hostingEnvironment;
        }

        public Stream OpenFile(string path)
        {
            var file = HostingEnvironment.ContentRootFileProvider.GetFileInfo(path);

            if(file == null)
            {
                return null;
            }

            return file.CreateReadStream();
        }
    }
}
