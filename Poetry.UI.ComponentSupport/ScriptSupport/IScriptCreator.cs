using Poetry.UI.ComponentSupport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.ScriptSupport
{
    public interface IScriptCreator
    {
        IEnumerable<Script> Create(string componentId, Type ownerType);
    }
}
