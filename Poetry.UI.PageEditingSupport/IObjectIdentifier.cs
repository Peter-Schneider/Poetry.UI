using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.PageEditingSupport
{
    public interface IObjectIdentifier
    {
        string GetId(object @object);
    }
}
