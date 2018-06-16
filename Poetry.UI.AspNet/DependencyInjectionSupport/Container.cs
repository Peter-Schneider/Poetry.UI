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

        public void RegisterType<T1, T2>() where T2 : T1
        {
            UnityContainer.RegisterType<T1, T2>();
        }
    }
}
