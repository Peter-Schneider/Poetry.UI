using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.MvcSupport
{
    public class ActionAttribute : Attribute
    {
        public string Id { get; }

        public ActionAttribute(string id)
        {
            Id = id;
        }
    }
}
