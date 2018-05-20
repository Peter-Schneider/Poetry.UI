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

            var result = new FormFieldProvider(new Dictionary<string, IEnumerable<FormField>> { [key] = value }).GetAllFor(key);

            Assert.Same(value, result);
        }

        [Fact]
        public void ReturnsNullIfMissing()
        {
            Assert.Null(new FormFieldProvider(new Dictionary<string, IEnumerable<FormField>>()).GetAllFor("lorem-ipsum"));
        }
    }
}
