using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;
using System.Web.Hosting;
using Poetry.UI.EmbeddedResourceSupport;
using Poetry.UI.AppSupport;
using System.Reflection;
using Unity;
using Unity.AspNet.Mvc;
using Poetry.UI.FormSupport;

namespace Poetry.UI
{
    public static class Startup
    {
        /// <summary>
        /// Add Poetry UI to your application. Don't forget calling .Done() when you're done configuring!
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public static PoetryConfigurator AddPoetryUI(this HttpApplication application)
        {
            var configurator = new PoetryConfigurator(new UnityContainer());

            configurator.AddAssembly(Assembly.GetCallingAssembly());

            return configurator;
        }
    }
}
