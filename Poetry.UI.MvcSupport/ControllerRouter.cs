using Poetry.UI.ComponentSupport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.MvcSupport
{
    public class ControllerRouter
    {
        Dictionary<Component, Type> Controllers { get; }

        public ControllerRouter(Dictionary<Component, Type> controllers)
        {
            Controllers = controllers;
        }

        public object Route(string path)
        {
            throw new NotImplementedException();
        }
    }
}
