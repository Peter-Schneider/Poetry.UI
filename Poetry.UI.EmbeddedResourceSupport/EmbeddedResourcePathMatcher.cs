using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.EmbeddedResourceSupport
{
    public class EmbeddedResourcePathMatcher : IEmbeddedResourcePathMatcher
    {
        public bool Match(EmbeddedResourceAssembly embeddedResourceAssembly, EmbeddedResource embeddedResource, string path)
        {
            path = path.ToLower();

            var name = embeddedResource.Name;

            if(!name.StartsWith(embeddedResourceAssembly.Name + "."))
            {
                return false;
            }

            name = name.Substring(embeddedResourceAssembly.Name.Length + ".".Length);

            name = name.ToLower();

            if(path == name)
            {
                return true;
            }

            if(path.Length != name.Length)
            {
                return false;
            }

            for(var i = 0; i < path.Length; i++)
            {
                var a = path[i];
                var b = name[i];

                if ((a == '-' || a == '_') && (b == '-' || b == '_'))
                {
                    continue;
                }

                if ((a == '.' || a == '/') && (b == '.' || b == '/'))
                {
                    continue;
                }

                if (a != b)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
