using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.RoutingSupport
{
    public interface IControllerRouter
    {
        ControllerRouterResult Route(string path);
    }
}
