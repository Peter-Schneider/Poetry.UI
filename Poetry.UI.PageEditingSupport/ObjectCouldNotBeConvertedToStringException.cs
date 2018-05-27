using System;
using System.Runtime.Serialization;

namespace Poetry.UI.PageEditingSupport
{
    public class ObjectCouldNotBeConvertedToStringException : Exception
    {
        public ObjectCouldNotBeConvertedToStringException(string message) : base(message) { }
    }
}