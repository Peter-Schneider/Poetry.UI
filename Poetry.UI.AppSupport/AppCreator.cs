using Poetry.UI.ComponentSupport;
using Poetry.UI.ReflectionSupport;
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
        IComponentRepository ComponentRepository { get; }
        IAppTypeProvider AppTypeProvider { get; }
        ITranslationRepositoryCreator TranslationRepositoryCreator { get; }
        IScriptCreator ScriptCreator { get; }
        IStyleCreator StyleCreator { get; }

        public AppCreator(IComponentRepository componentRepository, IAppTypeProvider appTypeProvider, ITranslationRepositoryCreator translationRepositoryCreator, IScriptCreator scriptCreator, IStyleCreator styleCreator)
        {
            ComponentRepository = componentRepository;
            AppTypeProvider = appTypeProvider;
            TranslationRepositoryCreator = translationRepositoryCreator;
            ScriptCreator = scriptCreator;
            StyleCreator = styleCreator;
        }

        public IEnumerable<App> Create()
        {
            foreach (var component in ComponentRepository.GetAll())
            {
                foreach (var type in AppTypeProvider.GetTypes(component))
                {
                    var attribute = CustomAttributeExtensions.GetCustomAttribute<AppAttribute>(type);

                    var translationAttribute = CustomAttributeExtensions.GetCustomAttribute<TranslationsAttribute>(type);
                    var translations = translationAttribute != null ? TranslationRepositoryCreator.Create(component.Id + "/" + translationAttribute.Path) : new EmptyTranslationRepository();

                    yield return new App(
                        attribute.Id,
                        new AssemblyWrapper(type.Assembly),
                        scripts: ScriptCreator.Create(component.Id, type),
                        styles: StyleCreator.Create(type),
                        translations: translations
                    );
                }
            }
        }
    }
}
