using Poetry.UI.ReflectionSupport;
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
            Assert.Equal(new List<Type> { typeof(MyComponentClass) }, new ComponentTypeProvider(new AssemblyProvider(new List<AssemblyWrapper> { new AssemblyWrapper(new List<Type> { typeof(MyComponentClass) }) })).GetTypes());
        }
    }
}
