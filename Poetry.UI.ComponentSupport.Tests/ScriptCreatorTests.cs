using System;
using System.Linq;
using Xunit;

namespace Poetry.UI.ScriptSupport.Tests
{
    public class ScriptCreatorTests
    {
        [Fact]
        public void CreatesScripts()
        {
            var result = new ScriptCreator().Create("prefix", typeof(ScriptOwner));

            Assert.Single(result);
            Assert.Equal("prefix/lorem-ipsum", result.Single().Path);
        }

        [Script("lorem-ipsum")]
        public class ScriptOwner { }
    }
}
