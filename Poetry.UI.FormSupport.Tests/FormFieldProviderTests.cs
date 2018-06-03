using Moq;
using Poetry.UI.FormSupport.FormFieldSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Poetry.UI.FormSupport.Tests
{
    public class FormFieldProviderTests
    {
        [Fact]
        public void GetsFields()
        {
            var key = "lorem-ipsum";
            var value = Enumerable.Empty<FormField>();

            var formCreator = Mock.Of<IFormCreator>();

            Mock.Get(formCreator).Setup(c => c.Create()).Returns(new List<Form> { new Form(key, value) });

            var result = new FormFieldProvider(formCreator).GetAllFor(key);

            Assert.Equal(value, result);
        }

        [Fact]
        public void ReturnsNullIfMissing()
        {
            var formCreator = Mock.Of<IFormCreator>();

            Mock.Get(formCreator).Setup(c => c.Create()).Returns(new List<Form> { });

            Assert.Null(new FormFieldProvider(formCreator).GetAllFor("lorem-ipsum"));
        }
    }
}
