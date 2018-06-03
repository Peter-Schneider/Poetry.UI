using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.ComponentSupport.DependencySupport
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class DependencyAttribute : Attribute
    {
        public string Id { get; }

        public DependencyAttribute(string id)
        {
            Id = id;
        }
    }
}
