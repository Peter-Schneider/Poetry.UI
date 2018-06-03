using System;
using System.Collections.Generic;

namespace Poetry.UI.FormSupport
{
    public interface IFormTypeProvider
    {
        IEnumerable<Type> GetTypes();
    }
}