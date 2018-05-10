using Poetry.UI.FileSupport;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;

namespace Poetry.UI.AspNet.FileSupport
{
    public class FileProvider : IFileProvider
    {
        public Stream OpenFile(string path)
        {
            var mappedPath = HostingEnvironment.MapPath(path);

            if (!File.Exists(mappedPath))
            {
                return null;
            }

            return new FileStream(mappedPath, FileMode.Open);
        }
    }
}
