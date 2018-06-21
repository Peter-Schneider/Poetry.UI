using Moq;
using Poetry.UI.FormSupport.FormFieldSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Poetry.UI.FormSupport.Tests
{
    public class FormCreatorTests
    {
        [Fact]
        public void CreatesForm()
        {
            var result = new FormCreator(Mock.Of<IFormFieldCreator>()).Create(typeof(MyForm));

            Assert.Equal("lorem-ipsum", result.Id);
            Assert.Equal(typeof(MyForm), result.Type);
        }
    }
}
