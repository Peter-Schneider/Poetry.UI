using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Poetry.UI.StyleSupport
{
    public class StyleCreator : IStyleCreator
    {
        public IEnumerable<Style> Create(Type ownerType)
        {
            return ownerType.GetCustomAttributes<StyleAttribute>().Select(a => new Style(a.Path)).ToList();
        }
    }
}
