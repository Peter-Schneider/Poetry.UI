using Poetry.UI.ScriptSupport;
using Poetry.UI.StyleSupport;
using Poetry.UI.TranslationSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Poetry.UI.AppSupport //
{
    public class AppCreator
    {
        ITranslationRepositoryCreator TranslationRepositoryCreator { get; }

        public AppCreator(ITranslationRepositoryCreator translationRepositoryCreator)
        {
            TranslationRepositoryCreator = translationRepositoryCreator;
        }

        public IEnumerable<App> Create(IEnumerable<Assembly> assemblies)
        {
            var types = assemblies
                    .SelectMany(a => a.ExportedTypes);

            foreach (var type in types)
            {
                var attribute = CustomAttributeExtensions.GetCustomAttribute<AppAttribute>(type);

                if (attribute == null)
                {
                    continue;
                }

                var scripts = CustomAttributeExtensions.GetCustomAttributes<ScriptAttribute>(type).Select(s => new Script(s.Path));
                var styles = CustomAttributeExtensions.GetCustomAttributes<StyleAttribute>(type).Select(s => s.Href);
                var translationAttribute = CustomAttributeExtensions.GetCustomAttribute<TranslationsAttribute>(type);
                var translations = translationAttribute != null ? TranslationRepositoryCreator.Create(translationAttribute.Path) : new EmptyTranslationRepository();

                yield return new App(
                    attribute.Id,
                    scripts: scripts,
                    styles: styles,
                    translations: translations
                );
            }
        }
    }
}
