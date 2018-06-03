using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Poetry.UI.EmbeddedResourceSupport.Tests
{
    public class EmbeddedResourceProviderTests
    {
        [Fact]
        public void ReturnsNullOnMissingFiles()
        {
            var sut = (IEmbeddedResourceProvider)new EmbeddedResourceProvider(Mock.Of<IEmbeddedResourcePathMatcher>(), Mock.Of<IEmbeddedResourceAssemblyProvider>());

            Assert.Null(sut.GetFile("missing-file"));
        }

        [Fact]
        public void ReturnsMatchingFile()
        {
            var file = new EmbeddedResource("...");
            var assembly = new EmbeddedResourceAssembly("name", "lorem", null, new List<EmbeddedResource> { file });
            var matcher = Mock.Of<IEmbeddedResourcePathMatcher>();

            Mock.Get(matcher).Setup(m => m.Match(assembly, file, "ipsum")).Returns(true);

            var embeddedResourceAssemblyProvider = Mock.Of<IEmbeddedResourceAssemblyProvider>();

            Mock.Get(embeddedResourceAssemblyProvider).Setup(p => p.GetAll()).Returns(new List<EmbeddedResourceAssembly>
            {
                assembly,
            });

            var sut = (IEmbeddedResourceProvider)new EmbeddedResourceProvider(matcher, embeddedResourceAssemblyProvider);

            Assert.Same(file, sut.GetFile("lorem/ipsum"));
        }

        [Fact]
        public void ChecksBasePath()
        {
            var file = new EmbeddedResource("ipsum");
            var assembly = new EmbeddedResourceAssembly("name", "lorem", null, new List<EmbeddedResource> { file });

            var matcher = Mock.Of<IEmbeddedResourcePathMatcher>();
            Mock.Get(matcher).Setup(m => m.Match(It.IsAny<EmbeddedResourceAssembly>(), It.IsAny<EmbeddedResource>(), It.IsAny<string>())).Returns(true);

            var embeddedResourceAssemblyProvider = Mock.Of<IEmbeddedResourceAssemblyProvider>();

            Mock.Get(embeddedResourceAssemblyProvider).Setup(p => p.GetAll()).Returns(new List<EmbeddedResourceAssembly>
            {
                assembly,
            });

            var sut = (IEmbeddedResourceProvider)new EmbeddedResourceProvider(matcher, embeddedResourceAssemblyProvider);

            var result = sut.GetFile("wrong/ipsum");

            Assert.Null(result);
        }

        [Fact]
        public void MultipleAssemblies()
        {
            var file = new EmbeddedResource("file-2");
            var assembly = new EmbeddedResourceAssembly("name", "ipsum", null, new List<EmbeddedResource> { file });

            var matcher = Mock.Of<IEmbeddedResourcePathMatcher>();
            Mock.Get(matcher).Setup(m => m.Match(It.IsAny<EmbeddedResourceAssembly>(), It.IsAny<EmbeddedResource>(), It.IsAny<string>())).Returns<EmbeddedResourceAssembly, EmbeddedResource, string>((a, r, p) => r.Name == p);

            var embeddedResourceAssemblyProvider = Mock.Of<IEmbeddedResourceAssemblyProvider>();

            Mock.Get(embeddedResourceAssemblyProvider).Setup(p => p.GetAll()).Returns(new List<EmbeddedResourceAssembly>
            {
                new EmbeddedResourceAssembly("name", "lorem", null, new List<EmbeddedResource> { new EmbeddedResource("file-1") }),
                assembly,
            });

            var sut = (IEmbeddedResourceProvider)new EmbeddedResourceProvider(matcher, embeddedResourceAssemblyProvider);

            Assert.Null(sut.GetFile("lorem/file-2"));
            Assert.Null(sut.GetFile("ipsum/file-1"));

            var result = sut.GetFile("ipsum/file-2");

            Assert.Same(file, result);
        }

        [Fact]
        public void MultipleAssembliesWithSameName()
        {
            var file = new EmbeddedResource("file");
            var assembly = new EmbeddedResourceAssembly("name", "lorem", null, new List<EmbeddedResource> { file });
            var decoy = new EmbeddedResourceAssembly("name", "lorem", null, Enumerable.Empty<EmbeddedResource>());

            var matcher = Mock.Of<IEmbeddedResourcePathMatcher>();
            Mock.Get(matcher).Setup(m => m.Match(It.IsAny<EmbeddedResourceAssembly>(), It.IsAny<EmbeddedResource>(), It.IsAny<string>())).Returns<EmbeddedResourceAssembly, EmbeddedResource, string>((a, r, p) => r.Name == p);

            var embeddedResourceAssemblyProvider = Mock.Of<IEmbeddedResourceAssemblyProvider>();

            Mock.Get(embeddedResourceAssemblyProvider).Setup(p => p.GetAll()).Returns(new List<EmbeddedResourceAssembly>
            {
                decoy, 
                assembly,
            });

            var sut = (IEmbeddedResourceProvider)new EmbeddedResourceProvider(matcher, embeddedResourceAssemblyProvider);

            var result = sut.GetFile("lorem/file");

            Assert.Same(file, result);
        }

        [Fact]
        public void ThrowsOnNullOrEmptyBasePath()
        {
            Assert.Throws<ArgumentException>(() => new EmbeddedResourceAssembly("name", string.Empty, null, Enumerable.Empty<EmbeddedResource>()));
            Assert.Throws<ArgumentException>(() => new EmbeddedResourceAssembly("name", null, null, Enumerable.Empty<EmbeddedResource>()));
        }

        [Fact]
        public void ThrowsOnNullOrEmptyName()
        {
            Assert.Throws<ArgumentException>(() => new EmbeddedResourceAssembly(string.Empty, "basePath", null, Enumerable.Empty<EmbeddedResource>()));
            Assert.Throws<ArgumentException>(() => new EmbeddedResourceAssembly(null, "basePath", null, Enumerable.Empty<EmbeddedResource>()));
        }
    }
}
