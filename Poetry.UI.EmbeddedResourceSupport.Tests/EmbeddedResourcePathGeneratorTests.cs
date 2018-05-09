using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace Poetry.UI.EmbeddedResourceSupport.Tests
{
    public class EmbeddedResourcePathGeneratorTests
    {
        [Fact]
        public void ConvertsUppercaseFolders()
        {
            var result = new EmbeddedResourcePathGenerator().GeneratePath("Lorem.Ipsum.dolor.txt");

            Assert.StartsWith("Lorem/Ipsum", result);
        }

        [Fact]
        public void ConvertsUppercaseNonSegmentedFilename()
        {
            var result = new EmbeddedResourcePathGenerator().GeneratePath("Lorem.Ipsum.Dolor.txt");

            Assert.EndsWith("/Dolor.txt", result);
        }

        [Fact]
        public void ConvertsLowercaseNonSegmentedFilename()
        {
            var result = new EmbeddedResourcePathGenerator().GeneratePath("Lorem.Ipsum.dolor.txt");

            Assert.EndsWith("/dolor.txt", result);
        }

        [Fact]
        public void ConvertsLowercaseSegmentedFilename()
        {
            var result = new EmbeddedResourcePathGenerator().GeneratePath("Lorem.Ipsum.dolor.sit.amet");

            Assert.EndsWith("/dolor.sit.amet", result);
        }
    }
}
