using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.StyleSupport
{
    public interface IStyleCreator
    {
        IEnumerable<Style> Create(Type ownerType);
    }
}
