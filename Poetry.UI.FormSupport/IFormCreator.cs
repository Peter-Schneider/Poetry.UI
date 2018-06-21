using System;
using System.Collections.Generic;

namespace Poetry.UI.FormSupport
{
    public interface IFormCreator
    {
        Form Create(Type type);
    }
}