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
    public class AppCreator : IAppCreator
    {
        IAppTypeProvider AppTypeProvider { get; }
        ITranslationRepositoryCreator TranslationRepositoryCreator { get; }
        IScriptCreator ScriptCreator { get; }
        IStyleCreator StyleCreator { get; }

        public AppCreator(IAppTypeProvider appTypeProvider, ITranslationRepositoryCreator translationRepositoryCreator, IScriptCreator scriptCreator, IStyleCreator styleCreator)
        {
            AppTypeProvider = appTypeProvider;
            TranslationRepositoryCreator = translationRepositoryCreator;
            ScriptCreator = scriptCreator;
            StyleCreator = styleCreator;
        }

        public IEnumerable<App> Create()
        {
            foreach (var type in AppTypeProvider.GetTypes())
            {
                var attribute = CustomAttributeExtensions.GetCustomAttribute<AppAttribute>(type);

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
