using System.Collections;
using System.Collections.Generic;

namespace Poetry.UI.ComponentSupport.InitializerSupport
{
    public interface IInitializerProvider
    {
        IEnumerable<IInitializer> GetAll();
    }
}