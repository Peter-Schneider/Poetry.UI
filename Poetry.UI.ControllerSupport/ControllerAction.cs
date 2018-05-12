using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.ControllerSupport
{
    /// <summary>
    /// Represents a Poetry.UI controller action. Type is prefixed with Controller as to avoid collision with System.Action.
    /// </summary>
    public sealed class ControllerAction
    {
        public string Id { get; }

        public ControllerAction(string id)
        {
            Id = id;
        }
    }
}
