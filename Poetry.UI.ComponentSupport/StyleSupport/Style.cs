using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.StyleSupport
{
    public class Style
    {
        public string Path { get; }
        public string LocalPath { get; }

        public Style(string componentId, string path)
        {
            Path = componentId + "/" + path;
            LocalPath = path;
        }
    }
}
