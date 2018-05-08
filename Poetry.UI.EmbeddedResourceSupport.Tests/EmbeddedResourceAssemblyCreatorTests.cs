using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace Poetry.UI.EmbeddedResourceSupport.Tests
{
    public class EmbeddedResourceAssemblyCreatorTests
    {
        [Fact]
        public void CreatesCorrectPath()
        {
            var result = new EmbeddedResourceAssemblyCreator().Create("lorem", Assembly.GetExecutingAssembly());

            Assert.Equal("NewFolder/Level/file.segment.txt", result.EmbeddedResources.Single().Path);
        }
    }
}
