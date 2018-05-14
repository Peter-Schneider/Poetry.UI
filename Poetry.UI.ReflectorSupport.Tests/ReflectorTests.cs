using Moq;
using Poetry.UI.ReflectorSupport.ReflectorAttributeSupport;
using Poetry.UI.ReflectorSupport.ReflectorTypeSupport;
using System;
using System.Linq;
using Xunit;

namespace Poetry.UI.ReflectorSupport.Tests
{
    public class ReflectorTests
    {
        [Fact]
        public void GetReflectorTypeNullCheck()
        {
            Assert.Throws<ArgumentNullException>(() => new Reflector(null).GetReflectorType(null));
        }

        [Fact]
        public void CallsReflectorTypeCreator()
        {
            var reflectorTypeCreator = Mock.Of<IReflectorTypeCreator>();

            var reflectorType = new ReflectorType("type", Enumerable.Empty<Attribute>());

            Mock.Get(reflectorTypeCreator).Setup(c => c.CreateReflectorType(typeof(string))).Returns(reflectorType);

            Assert.Same(reflectorType, new Reflector(reflectorTypeCreator).GetReflectorType(typeof(string)));
        }

        [Fact]
        public void CachesResult()
        {
            var reflectorTypeCreator = Mock.Of<IReflectorTypeCreator>();

            Mock.Get(reflectorTypeCreator).Setup(c => c.CreateReflectorType(typeof(string))).Returns(() => new ReflectorType("type", Enumerable.Empty<Attribute>()));

            var sut = new Reflector(reflectorTypeCreator);

            Assert.Same(sut.GetReflectorType(typeof(string)), sut.GetReflectorType(typeof(string)));
        }
    }
}
