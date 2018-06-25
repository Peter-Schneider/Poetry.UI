using Poetry.UI.ComponentSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Poetry.UI.ScriptSupport
{
    public class ScriptCreator : IScriptCreator
    {
        public IEnumerable<Script> Create(string componentId, Type ownerType)
        {
            return ownerType.GetCustomAttributes<ScriptAttribute>().Select(a => new Script(componentId, a.Path)).ToList().AsReadOnly();
        }
    }
}
