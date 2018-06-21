using Poetry.UI.ReflectionSupport;
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
            var result = new FormTypeProvider(new AssemblyProvider(new List<AssemblyWrapper> { new AssemblyWrapper(new List<Type> { typeof(MyForm) }) })).GetAll();

            Assert.Single(result);
            Assert.Equal(typeof(MyForm), result.Single());
        }
    }
}
