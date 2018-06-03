using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace Poetry.UI.FormSupport.Tests
{
    public class FormTypeProviderTests
    {
        [Fact]
        public void GetsTypes()
        {
            var result = new FormTypeProvider(new List<Assembly> { Assembly.GetExecutingAssembly() }).GetTypes();

            Assert.Single(result);
            Assert.Equal(typeof(MyForm), result.Single());
        }
    }
}
