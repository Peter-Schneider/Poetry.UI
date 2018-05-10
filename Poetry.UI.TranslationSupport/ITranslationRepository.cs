using System.Collections.Generic;

namespace Poetry.UI.TranslationSupport
{
    public interface ITranslationRepository
    {
        IDictionary<string, string> GetAll(string language);
        string Get(string key, string language);
    }
}