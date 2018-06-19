using Poetry.UI.DependencyInjectionSupport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.TranslationSupport
{
    [DependencyInjector]
    public class TranslationSupportDependencyInjector : IDependencyInjector
    {
        public void InjectDependencies(IContainer container)
        {
            container.RegisterSingleton<ITranslationParser, XmlTranslationParser>();
            container.RegisterSingleton<ITranslationRepositoryCreator, TranslationRepositoryCreator>();
        }
    }
}
