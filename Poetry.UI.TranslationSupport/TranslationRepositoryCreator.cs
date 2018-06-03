using Poetry.UI.FileSupport;
using System;
using System.Collections.Generic;
using System.IO;
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

        public ITranslationRepository Create(string path)
        {
            using (var read = FileProvider.OpenFile(path))
            {
                if (read == null)
                {
                    throw new FileNotFoundException(path);
                }

                return new TranslationRepository(TranslationParser.Parse(read));
            }
        }
    }
}
