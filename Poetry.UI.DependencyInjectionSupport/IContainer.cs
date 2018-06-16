using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.DependencyInjectionSupport
{
    public interface IContainer
    {
        void RegisterType<T1, T2>() where T2 : T1;
    }
}
