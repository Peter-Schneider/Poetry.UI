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
        public void CreatesForms()
        {
            var result = new FormCreator(Mock.Of<IFormFieldCreator>()).Create(typeof(MyForm));

            Assert.Single(result);
            Assert.Equal("lorem-ipsum", result.Single().Id);
        }
    }
}
