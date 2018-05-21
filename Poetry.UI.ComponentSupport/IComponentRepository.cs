using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.ComponentSupport
{
    public interface IComponentRepository
    {
        IEnumerable<Component> GetAll();
    }
}
