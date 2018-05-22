using Poetry.UI.DataTableSupport.BackendSupport;
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
            var result = new BackendTypeProvider().GetTypes(new List<Assembly> { Assembly.GetExecutingAssembly() });

            Assert.Single(result);
            Assert.Equal(typeof(MyDataTableBackend), result.Single());
        }
    }
}
