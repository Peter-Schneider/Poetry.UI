using Poetry.UI.ComponentSupport;
using Poetry.UI.ControllerSupport;
using Poetry.UI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poetry.UI.MvcSupport
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
            return null;
        }
    }
}
