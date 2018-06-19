﻿using Poetry.UI.EmbeddedResourceSupport;
using System;
using System.Collections.Generic;
using System.IO;
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

        public ITranslationRepository Create(string path)
        {
            var file = EmbeddedResourceProvider.GetFile(path);

            if (file == null)
            {
                throw new FileNotFoundException(path);
            }

            using (var read = EmbeddedResourceProvider.Open(file))
            {
                return new TranslationRepository(TranslationParser.Parse(read));
            }
        }
    }
}
