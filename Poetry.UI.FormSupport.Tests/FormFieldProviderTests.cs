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

            var formProvider = Mock.Of<IFormProvider>();

            Mock.Get(formProvider).Setup(c => c.GetAll()).Returns(new List<Form> { new Form(key, typeof(object), value) });

            var result = new FormFieldProvider(formProvider).GetAllFor(key);

            Assert.Equal(value, result);
        }

        [Fact]
        public void ReturnsNullIfMissing()
        {
            var formProvider = Mock.Of<IFormProvider>();

            Mock.Get(formProvider).Setup(c => c.GetAll()).Returns(new List<Form> { });

            Assert.Null(new FormFieldProvider(formProvider).GetAllFor("lorem-ipsum"));
        }
    }
}
