using Poetry.UI.DependencyInjectionSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Poetry.UI.AspNet.DependencyInjectionSupport
{
    public class Container : IContainer
    {
        IUnityContainer UnityContainer { get; }

        public Container(IUnityContainer unityContainer)
        {
            UnityContainer = unityContainer;
        }

        public void RegisterSingleton<T1, T2>() where T1 : class where T2 : class, T1
        {
            UnityContainer.RegisterSingleton<T1, T2>();
        }

        public void RegisterSingleton<T>(T instance) where T : class
        {
            UnityContainer.RegisterInstance<T>(instance);
        }

        public void RegisterType<T1, T2>() where T1 : class where T2 : class, T1
        {
            UnityContainer.RegisterType<T1, T2>();
        }

        public void RegisterType(Type from, Type to)
        {
            UnityContainer.RegisterType(from, to);
        }
    }
}
