using System;
using System.Runtime.Serialization;

namespace Poetry.UI.PageEditingSupport
{
    public class UnsupportedPropertyExpression : Exception
    {
        public UnsupportedPropertyExpression(string message) : base(message) { }
    }
}