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
            var file = new EmbeddedResource("...");
            var assembly = new EmbeddedResourceAssembly("name", "lorem", null, file);
            var matcher = Mock.Of<IEmbeddedResourcePathMatcher>();

            Mock.Get(matcher).Setup(m => m.Match(assembly, file, "ipsum")).Returns(true);

            var sut = (IEmbeddedResourceProvider)new EmbeddedResourceProvider(matcher, assembly);

            Assert.Same(file, sut.GetFile("lorem/ipsum"));
        }

        [Fact]
        public void ChecksBasePath()
        {
            var file = new EmbeddedResource("ipsum");
            var assembly = new EmbeddedResourceAssembly("name", "lorem", null, file);

            var matcher = Mock.Of<IEmbeddedResourcePathMatcher>();
            Mock.Get(matcher).Setup(m => m.Match(It.IsAny<EmbeddedResourceAssembly>(), It.IsAny<EmbeddedResource>(), It.IsAny<string>())).Returns(true);

            var sut = (IEmbeddedResourceProvider)new EmbeddedResourceProvider(matcher, assembly);

            var result = sut.GetFile("wrong/ipsum");

            Assert.Null(result);
        }

        [Fact]
        public void MultipleAssemblies()
        {
            var file = new EmbeddedResource("file-2");
            var assembly = new EmbeddedResourceAssembly("name", "ipsum", null, file);

            var matcher = Mock.Of<IEmbeddedResourcePathMatcher>();
            Mock.Get(matcher).Setup(m => m.Match(It.IsAny<EmbeddedResourceAssembly>(), It.IsAny<EmbeddedResource>(), It.IsAny<string>())).Returns<EmbeddedResourceAssembly, EmbeddedResource, string>((a, r, p) => r.Name == p);

            var sut = (IEmbeddedResourceProvider)new EmbeddedResourceProvider(matcher, new EmbeddedResourceAssembly("name", "lorem", null, new EmbeddedResource("file-1")), assembly);

            Assert.Null(sut.GetFile("lorem/file-2"));
            Assert.Null(sut.GetFile("ipsum/file-1"));

            var result = sut.GetFile("ipsum/file-2");

            Assert.Same(file, result);
        }

        [Fact]
        public void ThrowsOnNullOrEmptyBasePath()
        {
            Assert.Throws<ArgumentException>(() => new EmbeddedResourceAssembly("name", string.Empty, null));
            Assert.Throws<ArgumentException>(() => new EmbeddedResourceAssembly("name", null, null));
        }

        [Fact]
        public void ThrowsOnNullOrEmptyName()
        {
            Assert.Throws<ArgumentException>(() => new EmbeddedResourceAssembly(string.Empty, "basePath", null));
            Assert.Throws<ArgumentException>(() => new EmbeddedResourceAssembly(null, "basePath", null));
        }

        [Fact]
        public void ThrowsOnDuplicateBasePaths()
        {
            Assert.Throws<Exception>(() => new EmbeddedResourceProvider(null, new EmbeddedResourceAssembly("name", "lorem", null), new EmbeddedResourceAssembly("name", "lorem", null)));
        }
    }
}
