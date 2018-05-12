using System;

namespace Poetry.UI.MvcSupport
{
    public class ControllerAttribute : Attribute
    {
        public string Id { get; }

        public ControllerAttribute(string id)
        {
            Id = id;
        }
    }
}
