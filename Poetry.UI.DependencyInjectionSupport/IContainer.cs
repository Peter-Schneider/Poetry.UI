using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.DependencyInjectionSupport
{
    public interface IContainer
    {
        void RegisterSingleton<T1, T2>() where T1 : class where T2 : class, T1;
        void RegisterSingleton<T>(T instance) where T : class;
        void RegisterType<T1, T2>() where T1 : class where T2 : class, T1;
        void RegisterType(Type from, Type to);
    }
}
