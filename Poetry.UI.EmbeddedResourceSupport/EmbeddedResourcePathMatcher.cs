using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.EmbeddedResourceSupport
{
    public class EmbeddedResourcePathMatcher : IEmbeddedResourcePathMatcher
    {
        public bool Match(string path, string name)
        {
            path = path.ToLower();
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
