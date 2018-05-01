using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poetry.UI.MvcSupport
{
    public class BasePathProvider : IBasePathProvider
    {
        public string BasePath { get; }

        public BasePathProvider(string basePath)
        {
            BasePath = basePath;
        }
    }
}
