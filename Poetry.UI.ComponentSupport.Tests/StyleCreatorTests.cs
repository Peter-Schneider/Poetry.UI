using System;
using System.Linq;
using Xunit;

namespace Poetry.UI.StyleSupport.Tests
{
    public class StyleCreatorTests
    {
        [Fact]
        public void CreatesScripts()
        {
            var result = new StyleCreator().Create(typeof(StyleOwner));

            Assert.Single(result);
            Assert.Equal("lorem-ipsum", result.Single().Path);
        }

        [Style("lorem-ipsum")]
        public class StyleOwner { }
    }
}
