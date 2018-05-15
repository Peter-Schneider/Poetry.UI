using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.ControllerSupport
{
    public interface IControllerCreator
    {
        Controller Create(Type type);
    }
}
