using Poetry.UI.MvcSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Poetry.UI.ComponentSupport.Tests
{
    public class ComponentControllerTypeProviderTests
    {
        [Fact]
        public void FiltersTypes()
        {
            var result = new ComponentControllerTypeProvider().GetTypes(typeof(ComponentControllerTypeProviderTests));

            Assert.Single(result);
            Assert.Same(typeof(MyCorrectlySuffixedAndAttributedController), result.Single());
        }
    }

    [Controller("a")]
    public class MyCorrectlySuffixedAndAttributedController { }

    [Controller("b")]
    public class ControllerWithoutCorrectSuffix { }

    public class MyCorrectlySuffixedButNotAttributedController { }
}
