using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Poetry.UI.RoutingSupport.Tests
{
    public class MethodParameterProviderTests
    {
        [Fact]
        public void CreatesParameters()
        {
            var value = "lorem-ipsum";
            var valueProvider = Mock.Of<IMethodParameterValueProvider>();

            Mock.Get(valueProvider).Setup(p => p.GetValue(typeof(MyType).GetMethod(nameof(MyType.MyMethod)).GetParameters().Single())).Returns(value);

            var result = new MethodParameterProvider(valueProvider).GetParameters(typeof(MyType).GetMethod(nameof(MyType.MyMethod)));

            Assert.Single(result);
            Assert.Equal(value, result.Single());
        }

        public class MyType
        {
            public void MyMethod(string parameter) { }
        }
    }
}
