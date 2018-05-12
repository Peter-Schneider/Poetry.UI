using Poetry.UI.ComponentSupport;
using Poetry.UI.ControllerSupport;

namespace Poetry.UI.MvcSupport
{
    public class ControllerRouterResult
    {
        public Component Component { get; }
        public Controller Controller { get; }
        public ControllerAction Action { get; }

        public ControllerRouterResult(Component component, Controller controller, ControllerAction action)
        {
            Component = component;
            Controller = controller;
            Action = action;
        }
    }
}