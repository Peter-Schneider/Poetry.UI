using System;
using System.Collections.Generic;

namespace Poetry.UI.FormSupport.FormFieldSupport
{
    public interface IFormFieldCreator
    {
        IEnumerable<FormField> Create(Type formType);
    }
}