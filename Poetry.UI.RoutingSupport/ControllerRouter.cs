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

        public ControllerRouter(IBasePathProvider basePathProvider, params Component[] components)
        {
            BasePathProvider = basePathProvider;
            Components = components;
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

            var component = GetMatchingComponent(pathSegments.Dequeue());

            if(component == null)
            {
                return null;
            }

            var controller = GetMatchingController(component, pathSegments.Dequeue());

            if(controller == null)
            {
                return null;
            }

            var action = GetMatchingAction(controller, pathSegments.Dequeue());

            if (action == null)
            {
                return null;
            }

            if (pathSegments.Any())
            {
                return null;
            }

            return new ControllerRouterResult(component, controller, action);
        }

        Component GetMatchingComponent(string id)
        {
            return Components.FirstOrDefault(c => c.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase));
        }

        Controller GetMatchingController(Component component, string id)
        {
            return component.Controllers.FirstOrDefault(c => c.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase));
        }

        ControllerAction GetMatchingAction(Controller controller, string id)
        {
            return controller.Actions.FirstOrDefault(a => a.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
