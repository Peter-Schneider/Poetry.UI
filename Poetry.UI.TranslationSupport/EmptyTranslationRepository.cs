using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Poetry.UI.TranslationSupport
{
    public class EmptyTranslationRepository : ITranslationRepository
    {
        IDictionary<string, string> Empty { get; } = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>());

        public string Get(string key, string language)
        {
            return null;
        }

        public IDictionary<string, string> GetAll(string language)
        {
            return Empty;
        }
    }
}
