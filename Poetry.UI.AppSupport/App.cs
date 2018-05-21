using Poetry.UI.TranslationSupport;
using Poetry.UI.ScriptSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Poetry.UI.StyleSupport;

namespace Poetry.UI.AppSupport
{
    public class App
    {
        public string Name { get; }
        public IEnumerable<Script> Scripts { get; }
        public IEnumerable<Style> Styles { get; }
        public ITranslationRepository Translations { get; }

        public App(string name, IEnumerable<Script> scripts, IEnumerable<Style> styles, ITranslationRepository translations)
        {
            Name = name;
            Scripts = scripts.ToList().AsReadOnly();
            Styles = styles.ToList().AsReadOnly();
            Translations = translations;
        }
    }
}
