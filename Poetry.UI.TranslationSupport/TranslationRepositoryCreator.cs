using Poetry.UI.EmbeddedResourceSupport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.TranslationSupport
{
    public class TranslationRepositoryCreator : ITranslationRepositoryCreator
    {
        IEmbeddedResourceProvider EmbeddedResourceProvider { get; }
        ITranslationParser TranslationParser { get; }

        public TranslationRepositoryCreator(IEmbeddedResourceProvider embeddedResourceProvider, ITranslationParser translationParser)
        {
            EmbeddedResourceProvider = embeddedResourceProvider;
            TranslationParser = translationParser;
        }

        public TranslationRepository Create(string path)
        {
            var file = EmbeddedResourceProvider.GetFile(path);

            if(file == null)
            {
                throw new Exception($"Could not find embedded resource {path}");
            }

            using (var read = file.Open())
            {
                return new TranslationRepository(TranslationParser.Parse(read));
            }
        }
    }
}
