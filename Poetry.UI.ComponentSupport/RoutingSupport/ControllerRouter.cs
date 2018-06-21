using Poetry.UI.ComponentSupport;
using Poetry.UI.ControllerSupport;
using Poetry.UI.RoutingSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poetry.UI.RoutingSupport
{
    public class ControllerRouter : IControllerRouter
    {
        IBasePathProvider BasePathProvider { get; }
        IComponentRepository ComponentRepository { get; }

        public ControllerRouter(IBasePathProvider basePathProvider, IComponentRepository componentRepository)
        {
            BasePathProvider = basePathProvider;
            ComponentRepository = componentRepository;
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
                if(!pathSegments.Any() || pathSegments.Dequeue() != basePathSegment)
                {
                    return null;
                }
            }

            if(pathSegments.Count < 3)
            {
                return null;
            }

            var componentId = pathSegments.Dequeue();
            var controllerId = pathSegments.Dequeue();
            var actionId = pathSegments.Dequeue();

            if (pathSegments.Any())
            {
                return null;
            }

            foreach(var component in ComponentRepository.GetAll().Where(c => c.Id.Equals(componentId, StringComparison.InvariantCultureIgnoreCase)))
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
