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
        public void AddsAttributes()
        {
            Assert.Single(new ReflectorAttributeCreator().CreateReflectorAttributes(typeof(ClassWithAttribute)));
        }

        [Serializable]
        class ClassWithAttribute
        {

        }
    }
}
