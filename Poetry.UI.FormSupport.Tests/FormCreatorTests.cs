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
            var formTypeProvider = Mock.Of<IFormTypeProvider>();

            Mock.Get(formTypeProvider).Setup(p => p.GetTypes()).Returns(new List<Type> { typeof(MyForm) });

            var result = new FormCreator(Mock.Of<IFormFieldCreator>(), formTypeProvider).Create();

            Assert.Single(result);
            Assert.Equal("lorem-ipsum", result.Single().Id);
        }
    }
}
