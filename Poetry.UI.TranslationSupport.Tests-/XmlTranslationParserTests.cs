using System;
using System.IO;
using System.Text;
using Xunit;

namespace Poetry.UI.TranslationSupport.Tests
{
    public class XmlTranslationParserTests
    {
        [Fact]
        public void Works()
        {
            var xml = @"<xml><en><lorem>a</lorem></en><sv><ipsum>b</ipsum></sv></xml>";

            using(var read = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                var result = new XmlTranslationParser().Parse(read);

                Assert.Equal("a", result["en"]["lorem"]);
                Assert.Equal("b", result["sv"]["ipsum"]);
            }
        }
    }
}
