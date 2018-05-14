using Moq;
using Poetry.UI.ReflectorSupport.ReflectorAttributeSupport;
using Poetry.UI.ReflectorSupport.ReflectorTypeSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Poetry.UI.ReflectorSupport.Tests
{
    public class ReflectorTypeCreatorTests
    {
        [Fact]
        public void NullCheck()
        {
            Assert.Throws<ArgumentNullException>(() => new ReflectorTypeCreator(Mock.Of<IReflectorAttributeCreator>()).CreateReflectorType(null));
        }

        [Fact]
        public void CreatesType()
        {
            Assert.NotNull(new ReflectorTypeCreator(Mock.Of<IReflectorAttributeCreator>()).CreateReflectorType(typeof(string)));
        }

        [Fact]
        public void SetsId()
        {
            Assert.Equal("String", new ReflectorTypeCreator(Mock.Of<IReflectorAttributeCreator>()).CreateReflectorType(typeof(string)).Id);
        }

        [Fact]
        public void CallsReflectorAttributeCreator()
        {
            var attribute = new SerializableAttribute();

            var creator = Mock.Of<IReflectorAttributeCreator>();

            Mock.Get(creator).Setup(c => c.CreateReflectorAttributes(typeof(string))).Returns(new List<Attribute> { attribute });

            var result = new ReflectorTypeCreator(creator).CreateReflectorType(typeof(string));

            Assert.Same(attribute, result.Attributes.Single());
        }
    }
}
