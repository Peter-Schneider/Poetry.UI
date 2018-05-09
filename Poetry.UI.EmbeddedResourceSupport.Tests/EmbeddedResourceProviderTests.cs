using Moq;
using System;
using Xunit;

namespace Poetry.UI.EmbeddedResourceSupport.Tests
{
    public class EmbeddedResourceProviderTests
    {
        [Fact]
        public void ReturnsNullOnMissingFiles()
        {
            var sut = (IEmbeddedResourceProvider)new EmbeddedResourceProvider(Mock.Of<IEmbeddedResourcePathMatcher>());

            Assert.Null(sut.GetFile("missing-file"));
        }

        [Fact]
        public void ReturnsMatchingFile()
        {
            var file = new EmbeddedResource("ipsum", null);
            var assembly = new EmbeddedResourceAssembly("lorem", file);
            var matcher = Mock.Of<IEmbeddedResourcePathMatcher>();

            Mock.Get(matcher).Setup(m => m.Match("...", file.Name)).Returns(true);

            var sut = (IEmbeddedResourceProvider)new EmbeddedResourceProvider(matcher, assembly);

            Assert.Same(file, sut.GetFile("lorem/..."));
        }

        [Fact]
        public void ChecksBasePath()
        {
            var file = new EmbeddedResource("ipsum", null);
            var assembly = new EmbeddedResourceAssembly("lorem", file);

            var matcher = Mock.Of<IEmbeddedResourcePathMatcher>();
            Mock.Get(matcher).Setup(m => m.Match(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            var sut = (IEmbeddedResourceProvider)new EmbeddedResourceProvider(matcher, assembly);

            var result = sut.GetFile("wrong/ipsum");

            Assert.Null(result);
        }

        [Fact]
        public void MultipleAssemblies()
        {
            var file = new EmbeddedResource("file-2", null);
            var assembly = new EmbeddedResourceAssembly("ipsum", file);

            var matcher = Mock.Of<IEmbeddedResourcePathMatcher>();
            Mock.Get(matcher).Setup(m => m.Match(It.IsAny<string>(), It.IsAny<string>())).Returns<string, string>((p, n) => p == n);

            var sut = (IEmbeddedResourceProvider)new EmbeddedResourceProvider(matcher, new EmbeddedResourceAssembly("lorem", new EmbeddedResource("file-1", null)), assembly);

            Assert.Null(sut.GetFile("lorem/file-2"));
            Assert.Null(sut.GetFile("ipsum/file-1"));

            var result = sut.GetFile("ipsum/file-2");

            Assert.Same(file, result);
        }

        [Fact]
        public void ThrowsOnNullOrEmptyBasePath()
        {
            Assert.Throws<ArgumentException>(() => new EmbeddedResourceAssembly(string.Empty));
            Assert.Throws<ArgumentException>(() => new EmbeddedResourceAssembly(null));
        }

        [Fact]
        public void ThrowsOnDuplicateBasePaths()
        {
            Assert.Throws<Exception>(() => new EmbeddedResourceProvider(null, new EmbeddedResourceAssembly("lorem"), new EmbeddedResourceAssembly("lorem")));
        }
    }
}
