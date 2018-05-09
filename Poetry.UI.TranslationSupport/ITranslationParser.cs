using System.Collections.Generic;
using System.IO;

namespace Poetry.UI.TranslationSupport
{
    public interface ITranslationParser
    {
        Dictionary<string, Dictionary<string, string>> Parse(Stream stream);
    }
}