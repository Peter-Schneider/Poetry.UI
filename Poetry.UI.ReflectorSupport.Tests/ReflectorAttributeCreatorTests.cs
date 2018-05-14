using Poetry.UI.AttributeSupport;
using Poetry.UI.ReflectorSupport.ReflectorAttributeSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Poetry.UI.ReflectorSupport.Tests
{
    public class ReflectorAttributeCreatorTests
    {
        [Fact]
        public void AttributeContainer()
        {
            var result = new ReflectorAttributeCreator().CreateReflectorAttributes(typeof(ClassWithAttributeContainer));
            Assert.Single(result);
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IReflectorAttributeContainer>(result.Single());
            Assert.Equal("Serializable", result.Single().Name);
            Assert.IsType<SerializableAttribute>(((IReflectorAttributeContainer)result.Single()).Instance);
        }

        [Serializable]
        class ClassWithAttributeContainer
        {

        }

        [Fact]
        public void AttributeData()
        {
            var result = new ReflectorAttributeCreator().CreateReflectorAttributes(typeof(ClassWithAttributeData));
            Assert.Single(result);
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IReflectorAttributeData>(result.Single());
            Assert.Equal("lorem-ipsum", result.Single().Name);
            Assert.Equal(2, ((IReflectorAttributeData)result.Single()).Attributes.Count);
            Assert.Equal("lorem", ((IReflectorAttributeData)result.Single()).Attributes.Keys.First());
            Assert.Equal("ipsum", ((IReflectorAttributeData)result.Single()).Attributes.Values.First());
            Assert.Equal("dolor", ((IReflectorAttributeData)result.Single()).Attributes.Keys.Last());
            Assert.Equal("sit amet", ((IReflectorAttributeData)result.Single()).Attributes.Values.Last());
        }

        [AttributeWithData]
        class ClassWithAttributeData
        {

        }

        class AttributeWithDataAttribute : Attribute, IAttribute
        {
            public string Name => "lorem-ipsum";

            public Dictionary<string, string> Data => new Dictionary<string, string>
            {
                ["lorem"] = "ipsum",
                ["dolor"] = "sit amet",
            };
        }
    }
}
