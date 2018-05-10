using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Poetry.UI.TranslationSupport
{
    public class TranslationRepository : ITranslationRepository
    {
        IDictionary<string, string> Empty { get; } = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>());
        IDictionary<string, IDictionary<string, string>> Languages { get; }

        public TranslationRepository(Dictionary<string, Dictionary<string, string>> languages)
        {
            Languages = new ReadOnlyDictionary<string, IDictionary<string, string>>(languages.ToDictionary(p => p.Key, p => (IDictionary<string, string>)new ReadOnlyDictionary<string, string>(p.Value)));
        }

        public IDictionary<string, string> GetAll(string language)
        {
            if (!Languages.ContainsKey(language))
            {
                return Empty;
            }

            return Languages[language];
        }

        public string Get(string key, string language)
        {
            if (!Languages.ContainsKey(language))
            {
                return null;
            }

            if (!Languages[language].ContainsKey(key))
            {
                return null;
            }


            return Languages[language][key];
        }
    }
}
