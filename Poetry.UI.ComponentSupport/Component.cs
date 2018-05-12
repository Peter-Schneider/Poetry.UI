using Poetry.UI.ControllerSupport;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public string Id { get; }
        public IEnumerable<Controller> Controllers { get; }

        public Component(string id, params Controller[] controllers)
        {
            Id = id;
            Controllers = controllers.ToList().AsReadOnly();
        }
    }
}
