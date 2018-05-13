using Moq;
using Poetry.UI.ReflectorSupport;
using System;
using System.Collections.Generic;
using Xunit;

namespace Poetry.UI.ComponentSupport.Tests
{
    public class ComponentCreatorTests
    {
        [Fact]
        public void Works()
        {
            var reflector = Mock.Of<IReflector>();

            //Mock.Get(reflector).Setup(r => r.GetReflectorType(typeof(string))).Returns(new ReflectorType("string", new List<ReflectorAttribute> { new ReflectorAttribute("Component", new Dictionary<string, string> { ["Id"] = "my-component" }) }));

            Assert.NotNull(new ComponentCreator(null).Create(typeof(string)));
        }
    }
}
