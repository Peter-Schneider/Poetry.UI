using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Poetry.UI.EmbeddedResourceSupport.Tests
{
    public class EmbeddedResourcePathMatcherTests
    {
        [Fact]
        public void RejectsDifferentPaths()
        {
            Assert.False(new EmbeddedResourcePathMatcher().Match("Lorem", "Ipsum"));
        }

        [Fact]
        public void DotAndSlashInsensitive()
        {
            var sut = new EmbeddedResourcePathMatcher();

            Assert.True(sut.Match("Lorem.Ipsum.Dolor.hej", "Lorem/Ipsum.Dolor.hej"));
            Assert.True(sut.Match("Lorem.Ipsum.Dolor.hej", "Lorem.Ipsum/Dolor.hej"));
            Assert.True(sut.Match("Lorem.Ipsum.Dolor.hej", "Lorem.Ipsum.Dolor/hej"));
        }

        [Fact]
        public void CaseInsensitive()
        {
            var sut = new EmbeddedResourcePathMatcher();

            Assert.True(sut.Match("LoremIpsum", "loreMipsuM"));
        }

        [Fact]
        public void DashAndUnderscoreInsensitive()
        {
            var sut = new EmbeddedResourcePathMatcher();

            Assert.True(sut.Match("Lorem_Ipsum", "Lorem-Ipsum"));
            Assert.True(sut.Match("Lorem-Ipsum", "Lorem_Ipsum"));
        }
    }
}
