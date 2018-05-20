using Poetry.UI.FormSupport.FormFieldSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Poetry.UI.FormSupport.Tests
{
    public class FormFieldCreatorTests
    {
        [Fact]
        public void CreatesFieldsFromCompleteProperties()
        {
            var result = new FormFieldCreator().Create(typeof(TestForm));

            Assert.Single(result);
            Assert.Equal(nameof(TestForm.Property), result.Single().Id);
        }

        public class TestForm
        {
            public string Property { get; set; }
            public string OnlyGetter { get; }
            public string OnlySetter { set { } }
        }

        [Fact]
        public void SupportsString()
        {
            var result = new FormFieldCreator().Create(typeof(GenericForm<string>));

            Assert.Single(result);
            Assert.Equal("string", result.Single().Type);
        }

        [Fact]
        public void SupportsInt()
        {
            var result = new FormFieldCreator().Create(typeof(GenericForm<int>));

            Assert.Single(result);
            Assert.Equal("int", result.Single().Type);
        }

        [Fact]
        public void SupportsDouble()
        {
            var result = new FormFieldCreator().Create(typeof(GenericForm<double>));

            Assert.Single(result);
            Assert.Equal("double", result.Single().Type);
        }

        [Fact]
        public void SupportsBool()
        {
            var result = new FormFieldCreator().Create(typeof(GenericForm<bool>));

            Assert.Single(result);
            Assert.Equal("boolean", result.Single().Type);
        }

        public class GenericForm<T>
        {
            public T MyProperty { get; set; }
        }
    }
}
