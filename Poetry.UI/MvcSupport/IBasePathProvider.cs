using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poetry.UI.MvcSupport
{
    public interface IBasePathProvider
    {
        string BasePath { get; }
    }
}
