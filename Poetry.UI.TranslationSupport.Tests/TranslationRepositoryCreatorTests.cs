using Moq;
using Poetry.UI.EmbeddedResourceSupport;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace Poetry.UI.TranslationSupport.Tests
{
    public class TranslationRepositoryCreatorTests
    {
        [Fact]
        public void ThrowsOnMissingFile()
        {
            var sut = new TranslationRepositoryCreator(Mock.Of<IEmbeddedResourceProvider>(), null);

            Assert.Throws<FileNotFoundException>(() => sut.Create("missing-file"));
        }
    }
}
