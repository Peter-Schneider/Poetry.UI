using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poetry.UI.ResourceSupport
{
    /// <summary>
    /// Specifies that the embedded resource should be accessible over HTTP.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class ResourceAttribute : Attribute
    {
        public string Path { get; }

        public ResourceAttribute(string path)
        {
            Path = path;
        }
    }
}
