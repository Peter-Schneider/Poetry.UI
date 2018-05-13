using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poetry.UI.ScriptSupport
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class StyleAttribute : Attribute
    {
        public string Href { get; }

        public StyleAttribute(string href)
        {
            Href = href;
        }
    }
}
