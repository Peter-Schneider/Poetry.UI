using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Poetry.UI.TranslationSupport
{
    public class XmlTranslationParser : ITranslationParser
    {
        public Dictionary<string, Dictionary<string, string>> Parse(Stream stream)
        {
            var result = new Dictionary<string, Dictionary<string, string>>();

            var reader = XmlReader.Create(stream);
            Dictionary<string, string> translations = null;
            string key = null;

            while (reader.Read())
            {
                if (reader.Depth == 1 && reader.NodeType == XmlNodeType.Element)
                {
                    result[reader.LocalName] = translations = new Dictionary<string, string>();
                }

                if (reader.Depth == 2 && reader.NodeType == XmlNodeType.Element)
                {
                    key = reader.LocalName;
                }

                if (reader.Depth == 3 && reader.NodeType == XmlNodeType.Text)
                {
                    translations[key] = reader.Value;
                }
            }

            return result;
        }
    }
}
