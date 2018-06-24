using Poetry.UI.TranslationSupport;
using Poetry.UI.ScriptSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Poetry.UI.StyleSupport;
using Poetry.UI.ReflectionSupport;
using Poetry.UI.ComponentSupport;

namespace Poetry.UI.AppSupport
{
    public class App
    {
        public string Id { get; }
        public Component Component { get; }
        public IEnumerable<Script> Scripts { get; }
        public IEnumerable<Style> Styles { get; }
        public ITranslationRepository Translations { get; }

        public App(string id, Component component, IEnumerable<Script> scripts, IEnumerable<Style> styles, ITranslationRepository translations)
        {
            Id = id;
            Component = component;
            Scripts = scripts.ToList().AsReadOnly();
            Styles = styles.ToList().AsReadOnly();
            Translations = translations;
        }
    }
}
