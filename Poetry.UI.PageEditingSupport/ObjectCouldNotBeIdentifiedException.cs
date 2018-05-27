using System;
using System.Runtime.Serialization;

namespace Poetry.UI.PageEditingSupport
{
    public class ObjectCouldNotBeIdentifiedException : Exception
    {
        public ObjectCouldNotBeIdentifiedException(string message) : base(message) { }
    }
}