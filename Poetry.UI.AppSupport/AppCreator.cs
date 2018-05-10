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
                var attribute = type.GetTypeInfo().GetCustomAttribute<AppAttribute>();

                if (attribute == null)
                {
                    continue;
                }

                var typeInfo = type.GetTypeInfo();

                var scripts = typeInfo.GetCustomAttributes<ScriptAttribute>().Select(s => new Script(s.Src, s.Order));
                var styles = typeInfo.GetCustomAttributes<StyleAttribute>().Select(s => s.Href);
                var translationAttribute = typeInfo.GetCustomAttribute<TranslationsAttribute>();
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
