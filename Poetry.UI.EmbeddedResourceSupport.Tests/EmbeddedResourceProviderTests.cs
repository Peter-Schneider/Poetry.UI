using System;
using Xunit;

namespace Poetry.UI.EmbeddedResourceSupport.Tests
{
    public class EmbeddedResourceProviderTests
    {
        [Fact]
        public void ReturnsNullOnMissingFiles()
        {
            var sut = (IEmbeddedResourceProvider)new EmbeddedResourceProvider();

            Assert.Null(sut.GetFile("lorem ipsum (missing file)"));
        }

        [Fact]
        public void ReturnsCorrectFile()
        {
            var file = new EmbeddedResource("ipsum", null);
            var assembly = new EmbeddedResourceAssembly("lorem", file);
            var sut = (IEmbeddedResourceProvider)new EmbeddedResourceProvider(assembly);

            var result = sut.GetFile("lorem/ipsum");
            Assert.NotNull(result);
            Assert.Same(file, result);
        }

        [Fact]
        public void ChecksBasePath()
        {
            var file = new EmbeddedResource("ipsum", null);
            var assembly = new EmbeddedResourceAssembly("lorem", file);
            var sut = (IEmbeddedResourceProvider)new EmbeddedResourceProvider(assembly);

            var result = sut.GetFile("wrong/ipsum");

            Assert.Null(result);
        }

        [Fact]
        public void SupportsMultipleAssemblies()
        {
            var file1 = new EmbeddedResource("file-1", null);
            var assembly1 = new EmbeddedResourceAssembly("lorem", file1);

            var file2 = new EmbeddedResource("file-2", null);
            var assembly2 = new EmbeddedResourceAssembly("ipsum", file2);

            var sut = (IEmbeddedResourceProvider)new EmbeddedResourceProvider(assembly1, assembly2);

            Assert.Null(sut.GetFile("lorem/file-2"));
            Assert.Null(sut.GetFile("ipsum/file-1"));

            var result1 = sut.GetFile("lorem/file-1");

            Assert.NotNull(result1);
            Assert.Same(file1, result1);

            var result2 = sut.GetFile("ipsum/file-2");

            Assert.NotNull(result2);
            Assert.Same(file2, result2);
        }
    }
}
