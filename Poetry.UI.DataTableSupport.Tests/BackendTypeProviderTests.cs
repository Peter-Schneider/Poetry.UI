using Poetry.UI.DataTableSupport.BackendSupport;
using Poetry.UI.ReflectionSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Poetry.UI.DataTableSupport.Tests
{
    public class BackendTypeProviderTests
    {
        [Fact]
        public void GetsTypes()
        {
            var result = new BackendTypeProvider(new AssemblyProvider(new List<AssemblyWrapper> { new AssemblyWrapper(new List<Type> { typeof(MyDataTableBackend) }) })).GetTypes();

            Assert.Single(result);
            Assert.Equal(typeof(MyDataTableBackend), result.Single());
        }
    }
}
