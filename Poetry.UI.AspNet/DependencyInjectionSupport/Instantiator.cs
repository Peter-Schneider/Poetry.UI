using Poetry.UI.DependencyInjectionSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Poetry.UI.AspNet.DependencyInjectionSupport
{
    public class Instantiator : IInstantiator
    {
        public object Instantiate(Type type)
        {
            return DependencyResolver.Current.GetService(type);
        }
    }
}
