using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Poetry.UI.ControllerSupport;
using Poetry.UI.RoutingSupport;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Poetry.UI.AspNet.MvcSupport
{
    public class MvcController : System.Web.Mvc.Controller
    {
        public ActionResult Index()
        {
            var routingResult = ControllerContext.RouteData.DataTokens["PoetryRoutingResult"] as ControllerRouterResult;

            if (routingResult == null)
            {
                return HttpNotFound("Request was not routed through Poetry - something seems to be wrong in your routing.");
            }

            var instance = DependencyResolver.Current.GetService(routingResult.Controller.Type);

            IMethodParameterProvider controllerActionParameterProvider = new MethodParameterProvider(new MethodParameterValueProvider(Request.QueryString.AllKeys.ToDictionary(t => t, t => Request[t]), () => new StreamReader(Request.InputStream, Encoding.UTF8).ReadToEnd()));

            var parameters = controllerActionParameterProvider.GetParameters(routingResult.Action.Method);

            var result = routingResult.Action.Method.Invoke(instance, parameters);

            if(result != null)
            {
                return Content(JsonConvert.SerializeObject(result), "application/json");
            }
            else
            {
                return new EmptyResult();
            }
        }
    }
}
