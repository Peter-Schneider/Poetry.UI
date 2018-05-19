using Newtonsoft.Json;
using Poetry.UI.ControllerSupport;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Xunit;

namespace Poetry.UI.RoutingSupport.Tests
{
    public class MethodParameterValueProviderTests
    {
        [Fact]
        public void SupportsStringValue()
        {
            var value = "lorem-ipsum";

            var result = new MethodParameterValueProvider(new Dictionary<string, string> { ["parameter"] = value }, () => null).GetValue(typeof(TypeWithParameterMethod<string>).GetMethod("Method").GetParameters().Single());

            Assert.Equal(value, result);
        }

        [Fact]
        public void SupportsIntValue()
        {
            var value = 693;

            var result = new MethodParameterValueProvider(new Dictionary<string, string> { ["parameter"] = value.ToString(CultureInfo.InvariantCulture) }, () => null).GetValue(typeof(TypeWithParameterMethod<int>).GetMethod("Method").GetParameters().Single());

            Assert.Equal(value, result);
        }

        [Fact]
        public void SupportsDoubleValue()
        {
            var value = 134.235;

            var result = new MethodParameterValueProvider(new Dictionary<string, string> { ["parameter"] = value.ToString(CultureInfo.InvariantCulture) }, () => null).GetValue(typeof(TypeWithParameterMethod<double>).GetMethod("Method").GetParameters().Single());

            Assert.Equal(value, result);
        }

        [Fact]
        public void ReturnsNullIfNoValueExists()
        {
            var result = new MethodParameterValueProvider(new Dictionary<string, string> { }, () => null).GetValue(typeof(TypeWithParameterMethod<string>).GetMethod("Method").GetParameters().Single());

            Assert.Null(result);
        }

        [Fact]
        public void ReturnsNullIfUnknownType()
        {
            var result = new MethodParameterValueProvider(new Dictionary<string, string> { }, () => null).GetValue(typeof(TypeWithParameterMethod<object>).GetMethod("Method").GetParameters().Single());

            Assert.Null(result);
        }

        public class TypeWithParameterMethod<T>
        {
            public void Method(T parameter) { }
        }

        [Fact]
        public void SupportsJsonValueFromRequestBody()
        {
            var value = new MyClass { MyValue = "lorem-ipsum" };

            var result = new MethodParameterValueProvider(new Dictionary<string, string> { }, () => JsonConvert.SerializeObject(value)).GetValue(typeof(TypeWithParameterMethodFromRequestBody).GetMethod("Method").GetParameters().Single());

            Assert.Equal(JsonConvert.SerializeObject(value), JsonConvert.SerializeObject(result));
        }

        public class TypeWithParameterMethodFromRequestBody
        {
            public void Method([FromRequestBody] MyClass parameter) { }
        }

        public class MyClass
        {
            public string MyValue { get; set; }
        }
    }
}