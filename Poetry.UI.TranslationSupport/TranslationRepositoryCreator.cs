using Poetry.UI.FileSupport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poetry.UI.TranslationSupport
{
    public class TranslationRepositoryCreator : ITranslationRepositoryCreator
    {
        IFileProvider FileProvider { get; }
        ITranslationParser TranslationParser { get; }

        public TranslationRepositoryCreator(IFileProvider fileProvider, ITranslationParser translationParser)
        {
            FileProvider = fileProvider;
            TranslationParser = translationParser;
        }

        public TranslationRepository Create(string path)
        {
            var file = FileProvider.GetFile(path);

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
