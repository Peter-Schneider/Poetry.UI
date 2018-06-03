using Poetry.UI.ComponentSupport.DependencySupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Poetry.UI.ComponentSupport.Tests
{
    public class ComponentDependencyCreatorTests
    {
        [Fact]
        public void CreatesComponentDependencies()
        {
            var result = new ComponentDependencyCreator().Create(typeof(MyClass));

            Assert.Single(result);
            Assert.Equal("lorem-ipsum", result.Single());
        }

        [Dependency("lorem-ipsum")]
        public class MyClass { }
    }
}
