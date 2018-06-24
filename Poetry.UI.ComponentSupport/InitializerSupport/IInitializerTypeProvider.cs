using System;
using System.Collections.Generic;

namespace Poetry.UI.ComponentSupport.InitializerSupport
{
    public interface IInitializerTypeProvider
    {
        IEnumerable<Type> GetAll();
    }
}