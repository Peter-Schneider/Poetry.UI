using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Poetry.UI.DependencyInjectionSupport;
using Poetry.UI.RoutingSupport;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poetry.UI.AspNetCore.RoutingSupport
{
    public class ControllerMiddleware
    {
        IControllerRouter ControllerRouter { get; }
        IInstantiator Instantiator { get; }
        RequestDelegate Next { get; }

        public ControllerMiddleware(IControllerRouter controllerRouter, IInstantiator instantiator, RequestDelegate next)
        {
            ControllerRouter = controllerRouter;
            Instantiator = instantiator;
            Next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var routingResult = ControllerRouter.Route(httpContext.Request.Path.ToString());

            if (routingResult == null)
            {
                await Next(httpContext);
                return;
            }

            var instance = Instantiator.Instantiate(routingResult.Controller.Type);

            IMethodParameterProvider controllerActionParameterProvider = new MethodParameterProvider(new MethodParameterValueProvider(httpContext.Request.Query.Keys.ToDictionary(k => k, k => httpContext.Request.Query[k].First()), () => new StreamReader(httpContext.Request.Body, Encoding.UTF8).ReadToEnd()));

            var parameters = controllerActionParameterProvider.GetParameters(routingResult.Action.Method);

            var result = routingResult.Action.Method.Invoke(instance, parameters);

            if (result != null)
            {
                httpContext.Response.ContentType = "application/json";

                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(result));
            }
        }
    }
}
