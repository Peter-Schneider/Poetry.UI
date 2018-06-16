using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.ReflectionSupport
{
    public interface IAssemblyProvider
    {
        IEnumerable<AssemblyWrapper> GetAll();
    }
}
