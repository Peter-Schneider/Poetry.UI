using System;
using System.Collections.Generic;

namespace Poetry.UI.TranslationSupport
{
    public class TranslationRepository
    {
        public Dictionary<string, Dictionary<string, string>> Languages { get; }

        public TranslationRepository(Dictionary<string, Dictionary<string, string>> languages)
        {
            Languages = languages;
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
