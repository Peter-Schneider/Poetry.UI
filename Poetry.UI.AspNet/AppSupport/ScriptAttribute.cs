using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poetry.UI.AppSupport
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class ScriptAttribute : Attribute
    {
        public string Src { get; }
        public int Order { get; }

        public ScriptAttribute(string src)
        {
            Src = src;
        }

        public ScriptAttribute(string src, int order)
        {
            Src = src;
            Order = order;
        }
    }
}
