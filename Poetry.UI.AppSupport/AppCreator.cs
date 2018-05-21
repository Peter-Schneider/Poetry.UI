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
        IScriptCreator ScriptCreator { get; }
        IStyleCreator StyleCreator { get; }

        public AppCreator(ITranslationRepositoryCreator translationRepositoryCreator, IScriptCreator scriptCreator, IStyleCreator styleCreator)
        {
            TranslationRepositoryCreator = translationRepositoryCreator;
            ScriptCreator = scriptCreator;
            StyleCreator = styleCreator;
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

                var translationAttribute = CustomAttributeExtensions.GetCustomAttribute<TranslationsAttribute>(type);
                var translations = translationAttribute != null ? TranslationRepositoryCreator.Create(translationAttribute.Path) : new EmptyTranslationRepository();

                yield return new App(
                    attribute.Id,
                    scripts: ScriptCreator.Create(type),
                    styles: StyleCreator.Create(type),
                    translations: translations
                );
            }
        }
    }
}
