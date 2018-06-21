using Moq;
using Poetry.UI.FormSupport.FormFieldSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Poetry.UI.FormSupport.Tests
{
    public class FormProviderTests
    {
        [Fact]
        public void CreatesForms()
        {
            var formTypeProvider = Mock.Of<IFormTypeProvider>();

            Mock.Get(formTypeProvider).Setup(p => p.GetTypes()).Returns(new List<Type> { typeof(object) });

            var form = new Form("lorem-ipsum", typeof(object), Enumerable.Empty<FormField>());

            var formCreator = Mock.Of<IFormCreator>();

            Mock.Get(formCreator).Setup(c => c.Create(typeof(object))).Returns(form);

            Assert.Single(new FormProvider(formTypeProvider, formCreator).GetAll(), form);
        }
    }
}
