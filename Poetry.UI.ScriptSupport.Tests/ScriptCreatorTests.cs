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
            var result = new ScriptCreator().Create(typeof(ScriptOwner));

            Assert.Single(result);
            Assert.Equal("lorem-ipsum", result.Single().Path);
        }

        [Script("lorem-ipsum")]
        public class ScriptOwner { }
    }
}
