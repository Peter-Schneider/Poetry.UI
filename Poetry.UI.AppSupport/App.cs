using Poetry.UI.TranslationSupport;
using Poetry.UI.ScriptSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Poetry.UI.StyleSupport;
using Poetry.UI.ReflectionSupport;
using Poetry.UI.ComponentSupport;
using Poetry.UI.ResourceSupport;

namespace Poetry.UI.AppSupport
{
    public class App
    {
        public string Id { get; }
        public Component Component { get; }
        public IEnumerable<Script> Scripts { get; }
        public IEnumerable<Style> Styles { get; }
        public IEnumerable<Resource> Resources { get; }
        public ITranslationRepository Translations { get; }

        public App(string id, Component component, IEnumerable<Script> scripts, IEnumerable<Style> styles, IEnumerable<Resource> resources, ITranslationRepository translations)
        {
            Id = id;
            Component = component;
            Scripts = scripts.ToList().AsReadOnly();
            Styles = styles.ToList().AsReadOnly();
            Resources = resources.ToList().AsReadOnly();
            Translations = translations;
        }
    }
}
