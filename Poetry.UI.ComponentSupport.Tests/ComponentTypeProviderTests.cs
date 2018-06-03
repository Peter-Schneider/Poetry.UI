using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit;

namespace Poetry.UI.ComponentSupport.Tests
{
    public class ComponentTypeProviderTests
    {
        [Fact]
        public void GetsTypes()
        {
            Assert.Equal(new List<Type> { typeof(MyComponentClass) }, new ComponentTypeProvider(new List<Assembly> { typeof(MyComponentClass).Assembly }).GetTypes());
        }
    }
}
