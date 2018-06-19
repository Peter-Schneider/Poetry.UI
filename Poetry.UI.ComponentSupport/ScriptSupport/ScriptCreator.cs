using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Poetry.UI.ScriptSupport
{
    public class ScriptCreator : IScriptCreator
    {
        public IEnumerable<Script> Create(Type ownerType)
        {
            return ownerType.GetCustomAttributes<ScriptAttribute>().Select(a => new Script(a.Path)).ToList();
        }
    }
}
