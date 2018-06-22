using Poetry.UI.TranslationSupport;
using Poetry.UI.ScriptSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Poetry.UI.StyleSupport;
using Poetry.UI.ReflectionSupport;

namespace Poetry.UI.AppSupport
{
    public class App
    {
        public string Id { get; }
        public AssemblyWrapper Assembly { get; }
        public IEnumerable<Script> Scripts { get; }
        public IEnumerable<Style> Styles { get; }
        public ITranslationRepository Translations { get; }

        public App(string id, AssemblyWrapper assembly, IEnumerable<Script> scripts, IEnumerable<Style> styles, ITranslationRepository translations)
        {
            Id = id;
            Assembly = assembly;
            Scripts = scripts.ToList().AsReadOnly();
            Styles = styles.ToList().AsReadOnly();
            Translations = translations;
        }
    }
}
