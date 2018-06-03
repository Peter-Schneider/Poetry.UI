using Poetry.UI.ComponentSupport;
using Poetry.UI.ControllerSupport;
using Poetry.UI.RoutingSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poetry.UI.RoutingSupport
{
    public class ControllerRouter
    {
        IBasePathProvider BasePathProvider { get; }
        IEnumerable<Component> Components { get; }

        public ControllerRouter(IBasePathProvider basePathProvider, IEnumerable<Component> components)
        {
            BasePathProvider = basePathProvider;
            Components = components.ToList().AsReadOnly();
        }

        public ControllerRouterResult Route(string path)
        {
            if (path == "/")
            {
                return null;
            }

            var pathSegments = new Queue<string>(path.ToLower().Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries));

            foreach(var basePathSegment in BasePathProvider.BasePath.ToLower().Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if(pathSegments.Dequeue() != basePathSegment)
                {
                    return null;
                }
            }

            var componentId = pathSegments.Dequeue();
            var controllerId = pathSegments.Dequeue();
            var actionId = pathSegments.Dequeue();

            if (pathSegments.Any())
            {
                return null;
            }

            foreach(var component in Components.Where(c => c.Id.Equals(componentId, StringComparison.InvariantCultureIgnoreCase)))
            {
                foreach(var controller in component.Controllers.Where(c => c.Id.Equals(controllerId, StringComparison.InvariantCultureIgnoreCase)))
                {
                    var action = controller.Actions.FirstOrDefault(a => a.Id.Equals(actionId, StringComparison.InvariantCultureIgnoreCase));

                    if(action != null)
                    {
                        return new ControllerRouterResult(component, controller, action);
                    }
                }
            }

            return null;

        }
    }
}
