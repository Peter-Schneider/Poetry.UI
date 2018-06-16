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
            container.RegisterType<ITranslationParser, XmlTranslationParser>();
            container.RegisterType<ITranslationRepositoryCreator, TranslationRepositoryCreator>();
        }
    }
}
