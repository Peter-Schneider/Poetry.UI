using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poetry.UI.Core
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
