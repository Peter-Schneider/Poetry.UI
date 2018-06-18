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
        public string Name { get; }
        public AssemblyWrapper Assembly { get; }
        public IEnumerable<Script> Scripts { get; }
        public IEnumerable<Style> Styles { get; }
        public ITranslationRepository Translations { get; }

        public App(string name, AssemblyWrapper assembly, IEnumerable<Script> scripts, IEnumerable<Style> styles, ITranslationRepository translations)
        {
            Name = name;
            Assembly = assembly;
            Scripts = scripts.ToList().AsReadOnly();
            Styles = styles.ToList().AsReadOnly();
            Translations = translations;
        }
    }
}
