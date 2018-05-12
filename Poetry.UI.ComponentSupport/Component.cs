using Poetry.UI.ControllerSupport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.ComponentSupport
{
    /// <summary>
    /// Represents a Poetry.UI component.
    /// 
    /// Note: Not to be inherited by your component classes as they should be POCOs, annotated with the [Component] attribute.
    /// </summary>
    public sealed class Component
    {
        public IEnumerable<Controller> Controllers { get; }

        public Component(IEnumerable<Controller> controllers)
        {
            Controllers = controllers;
        }
    }
}
