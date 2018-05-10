using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Poetry.UI.EmbeddedResourceSupport.Tests
{
    public class EmbeddedResourcePathMatcherTests
    {
        [Fact]
        public void RejectsDifferentAssemblyNames()
        {
            Assert.False(new EmbeddedResourcePathMatcher().Match(new EmbeddedResourceAssembly("name", "basePath", null), new EmbeddedResource("1234.Lorem"), "Lorem"));
        }

        [Fact]
        public void MatchesAssemblyNames()
        {
            Assert.True(new EmbeddedResourcePathMatcher().Match(new EmbeddedResourceAssembly("name", "basePath", null), new EmbeddedResource("name.Lorem"), "Lorem"));
        }

        [Fact]
        public void RejectsDifferentPaths()
        {
            Assert.False(new EmbeddedResourcePathMatcher().Match(new EmbeddedResourceAssembly("name", "basePath", null), new EmbeddedResource("name.Lorem"), "Ipsum"));
        }

        [Fact]
        public void DotAndSlashInsensitive()
        {
            var sut = new EmbeddedResourcePathMatcher();

            Assert.True(sut.Match(new EmbeddedResourceAssembly("name", "basePath", null), new EmbeddedResource("name.Lorem.Ipsum.Dolor.hej"), "Lorem/Ipsum.Dolor.hej"));
            Assert.True(sut.Match(new EmbeddedResourceAssembly("name", "basePath", null), new EmbeddedResource("name.Lorem.Ipsum.Dolor.hej"), "Lorem.Ipsum/Dolor.hej"));
            Assert.True(sut.Match(new EmbeddedResourceAssembly("name", "basePath", null), new EmbeddedResource("name.Lorem.Ipsum.Dolor.hej"), "Lorem.Ipsum.Dolor/hej"));
        }

        [Fact]
        public void CaseInsensitive()
        {
            var sut = new EmbeddedResourcePathMatcher();

            Assert.True(sut.Match(new EmbeddedResourceAssembly("name", "basePath", null), new EmbeddedResource("name.LoremIpsum"), "loreMipsuM"));
        }

        [Fact]
        public void DashAndUnderscoreInsensitive()
        {
            var sut = new EmbeddedResourcePathMatcher();

            Assert.True(sut.Match(new EmbeddedResourceAssembly("name", "basePath", null), new EmbeddedResource("name.Lorem_Ipsum"), "Lorem-Ipsum"));
            Assert.True(sut.Match(new EmbeddedResourceAssembly("name", "basePath", null), new EmbeddedResource("name.Lorem-Ipsum"), "Lorem_Ipsum"));
        }
    }
}
